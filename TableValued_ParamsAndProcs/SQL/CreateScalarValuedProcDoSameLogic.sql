CREATE PROC learning_ScalarValuedProc(@integer int, @string NVARCHAR(32)) AS BEGIN
DECLARE @boolean BIT = (SELECT CASE WHEN @integer = 10 THEN 1 ELSE 0 END)
INSERT INTO TypesAndStuff (Integers,Strings,Booleans,Characters) VALUES
(
	@integer,
	@string,
	@boolean,
	LEFT(@string,1)
)
END