--This is a recursive sql function. It returns a table of items and
--the function is called again on rows of said returned table. 
--It also uses a CURSOR. This is like a for loop in SQL and is rarely used. 
--This would qualify as one of the rare times when a CURSOR is used/needed

USE [polyprint]
GO
/****** Object:  UserDefinedFunction [dbo].[WipRollComponents]    Script Date: 1/22/2018 11:59:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER FUNCTION [dbo].[WipRollComponents](@wipRollNumber int)
RETURNS @result Table (MaterialType char, barcode int) 
AS BEGIN

DECLARE @selectionSet Table (MaterialType char, barcode int)
INSERT INTO @selectionSet(MaterialType,barcode)
	SELECT
		WRF.Material_Type,
		WRF.Barcode
	FROM WipRoll WR
	INNER JOIN WipRollFilm WRF ON
		WR.Record_Number = WRF.WIP_Roll_Number
	WHERE WR.Record_Number = @wipRollNumber

DECLARE @materialType char
DECLARE @barcode int
DECLARE CURS CURSOR FOR SELECT MaterialType,barcode FROM @selectionSet
OPEN CURS
FETCH NEXT FROM CURS --Begins Iteration
INTO @materialType,@barcode;
WHILE @@FETCH_STATUS = 0 BEGIN
	IF(@materialType in ('A','B','C')) BEGIN
		INSERT INTO @result(MaterialType,barcode) VALUES (@materialType,@barcode) END
	ELSE BEGIN
		INSERT INTO @result(MaterialType,barcode) SELECT * FROM WipRollComponents(@barcode) 
	
		END

	IF NOT (@materialType in ('A','B','C')) 
		INSERT INTO @result(MaterialType,barcode) VALUES (@materialType,@barcode) 

	FETCH NEXT FROM CURS INTO @materialType,@barcode --Continues iteration. @@FETCH_STATUS != 0 when nothing else to fetch


END
RETURN
END



