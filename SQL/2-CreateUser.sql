
-- Create on the DB you created

CREATE USER classLogin
	FOR LOGIN classLogin
	WITH DEFAULT_SCHEMA = dbo
GO

-- Add user to the database owner role
EXEC sp_addrolemember N'db_datareader', N'classLogin'
GO