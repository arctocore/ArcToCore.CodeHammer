SELECT
    INFORMATION_SCHEMA.COLUMNS.*,
    COL_LENGTH('#TableName#', INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME) AS COLUMN_LENGTH,
    COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsComputed') AS IS_COMPUTED,
    COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsIdentity') AS IS_IDENTITY,
    COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsRowGuidCol') AS IS_ROWGUIDCOL,
    COLUMNPROPERTY(OBJECT_ID('#TableName#'), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsNullable') AS IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS

order BY TABLE_NAME



    select object_name(i.object_id) tablename, c.name colname, c.is_identity 
, idc.increment_value
from sys.indexes i
inner join sys.index_columns ic on ic.object_id = i.object_id and ic.index_id = i.index_id
inner join sys.columns c on c.object_id = ic.object_id and c.column_id = ic.column_id
left outer join sys.identity_columns  idc on idc.object_id = c.object_id and idc.column_id = c.column_id
where i.is_primary_key = 1


SELECT
    PK_Table = PK.TABLE_NAME,
    PK_Column = PT.COLUMN_NAME,
    --K_Table = FK.TABLE_NAME,
    FK_Column = CU.COLUMN_NAME

   -- Constraint_Name = C.CONSTRAINT_NAME
FROM
    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK
    ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK
    ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU
    ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
INNER JOIN (
            SELECT
                i1.TABLE_NAME,
                i2.COLUMN_NAME
            FROM
                INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2
                ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
            WHERE
                i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
           ) PT
    ON PT.TABLE_NAME = PK.TABLE_NAME
    order by PK_Table asc



