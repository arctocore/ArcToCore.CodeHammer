

USE #DatabaseName#

/******************************************************************************************
Create the #UserName# login.
******************************************************************************************/
if not exists (SELECT
	*
FROM master..syslogins
WHERE name = '#UserName#')
EXEC sp_addlogin	'#UserName#',
					'',
					'#DatabaseName#'
GO

/******************************************************************************************
Grant the #UserName# login access to the #DatabaseName# database.
******************************************************************************************/
if not exists (SELECT
	*
FROM sysusers
WHERE name = N'#UserName#' AND uid < 16382)
EXEC sp_grantdbaccess	N'#UserName#',
						N'#UserName#'
GO
