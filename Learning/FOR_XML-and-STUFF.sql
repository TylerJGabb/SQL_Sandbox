
CREATE TABLE #table
(
	String1 NVARCHAR(50),
	String2 NVARCHAR(50),
)

INSERT INTO #table VALUES ('a','A')
INSERT INTO #table VALUES ('b','B')
INSERT INTO #table VALUES ('c','C')

-- EXAMPLE TURN TABLE INTO JSON STRING
DECLARE @json NVARCHAR(MAX)
SELECT @json = CONVERT(VARCHAR(MAX), 
(
	SELECT
		String1,
		String2
	FROM #table
	FOR JSON PATH, ROOT('TABLE1')
)) 

--EXAMPLE TURN TABLE INTO XML STRING
DECLARE @xml NVARCHAR(MAX)
SELECT @xml = CONVERT (VARCHAR(MAX),
(
	SELECT
		',' + String1,
		'' + String2
	FROM #table
	FOR XML PATH(''), TYPE

)) 
DROP TABLE #table


--AN EXAMPLE OF USING STUFF
DECLARE @result NVARCHAR(MAX)
DECLARE @expression NVARCHAR(MAX) = 'xCOLUMN'
DECLARE @startPos INT = 1
DECLARE @numCharsToDel INT = 1
DECLARE @replacement NVARCHAR(MAX) = 'y'
SELECT @result = STUFF(@expression,@startPos,@numCharsToDel,@replacement)
--https://docs.microsoft.com/en-us/sql/t-sql/functions/stuff-transact-sql

SELECT
	@json [JSON],
	@xml [XML],
	@result [STUFF_RESULT]

