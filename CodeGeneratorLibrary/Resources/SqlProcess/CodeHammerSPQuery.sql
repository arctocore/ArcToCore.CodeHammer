

SELECT
	NAME,
	OBJECT_ID
FROM sys.all_objects
WHERE (is_ms_shipped = 0) AND (type_desc = 'SQL_STORED_PROCEDURE')
ORDER BY NAME
