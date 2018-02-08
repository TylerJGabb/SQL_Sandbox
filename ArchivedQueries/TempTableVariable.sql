DECLARE @tempTable TABLE
(
	Number DECIMAL(18),
	String NVARCHAR(32),
	Field1 NVARCHAR(32),
	Field2 NVARCHAR(32)
)

INSERT INTO @tempTable VALUES (1,'1','2','3')

SELECT * FROM @tempTable