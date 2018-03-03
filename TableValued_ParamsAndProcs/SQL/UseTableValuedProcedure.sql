
--DECLARE THE TestTable @tableParam
DECLARE @tableParam AS TestTable

--PUT SOME STUFF IN IT
INSERT INTO @tableParam (Integers,Strings) 
	SELECT 1234, '1234'

INSERT INTO @tableParam (Integers,Strings) 
	SELECT 5678, '5678'

--TAKE A LOOK AT WHATS INSIDE
SELECT * FROM @tableParam

--EXECUTE THE STORED PROCEDURE
EXEC learning_TableValuedProc @tableParam
SELECT * FROM TypesAndStuff