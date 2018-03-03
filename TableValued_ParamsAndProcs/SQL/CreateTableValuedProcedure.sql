--CREATE A TABLE VALUED PROCEDURE
ALTER PROC learning_TableValuedProc(@tableValuedParam TestTable READONLY) AS BEGIN
	INSERT INTO TypesAndStuff (Integers, Strings, Booleans, Characters )
	SELECT 
		Integers,
		Strings,
		Integers = 10,
		LEFT(Strings,1)
	FROM @tableValuedParam
	SELECT @@ROWCOUNT
END
/*This procedure can now accept a table, declared of type TestTable. See Programability for table definition*/
