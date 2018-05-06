/*
The end goal of the query in this file is to be able to specify an arbitrary table, and drop and recreate it
dynamically
*/

DECLARE @tableName NVARCHAR(128) = 'People'
DECLARE @sql NVARCHAR(MAX)


CREATE TABLE #columns
(
	[COLUMN_DEFINITION] NVARCHAR(MAX)
)

INSERT INTO #columns
SELECT
	'[' + COLUMN_NAME + '] ' + DATA_TYPE + CASE WHEN CHARACTER_MAXIMUM_LENGTH IS NULL THEN '' ELSE '(' + CONVERT(NVARCHAR(10),CHARACTER_MAXIMUM_LENGTH) + ')' END  [COLUMN]
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = @tableName
ORDER BY ORDINAL_POSITION ASC


SELECT 
	',' + COLUMN_DEFINITION 
FROM #columns 
FOR XML PATH(''),
TYPE

--https://docs.microsoft.com/en-us/sql/t-sql/xml/value-method-xml-data-type --.value()
--https://docs.microsoft.com/en-us/sql/t-sql/queries/select-for-clause-transact-sql --SELECT
SELECT @sql = STUFF((SELECT ', ' + COLUMN_DEFINITION FROM #columns FOR XML PATH(''), TYPE).value('text()[1]','nvarchar(max)'),1,2,'')
SELECT @sql [SQL]

SET @sql = 'CREATE TABLE #temp (' + @sql + '); INSERT INTO #temp SELECT * FROM People; SELECT * FROM #temp; DROP TABLE #temp'
SELECT @sql
EXEC(@sql)--#temp only exists in the scope of this query. not outside. 
--INSERT INTO #temp SELECT * FROM People


DROP TABLE #columns

--SELECT
--	*
--FROM INFORMATION_SCHEMA.COLUMNS
--WHERE TABLE_NAME = @tableName
--ORDER BY ORDINAL_POSITION ASC



