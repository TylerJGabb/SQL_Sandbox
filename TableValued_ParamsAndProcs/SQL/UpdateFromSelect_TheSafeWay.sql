DECLARE @materialScan AS InventoryScanTable

INSERT INTO @materialScan (PREFIX,BARCODE,LOCATION) VALUES ('R',10009,'LOC-09')
INSERT INTO @materialScan (PREFIX,BARCODE,LOCATION) VALUES ('P',10001,'LOC-01')
SELECT * FROM @materialScan
/*Notice that we have removed some rows as to demonstrate selecting only the data we want to change*/


/*First, select the information you want to change based on the incoming scan table*/
SELECT
	ML.*
FROM MaterialLocations ML
INNER JOIN @materialScan S ON S.BARCODE = ML.BARCODE
/*See that only the rows we wish to update are brought back*/

/*Now change the select statement into an update statement*/
UPDATE ML
--SELECT
	--ML.*
SET LOCATION = S.LOCATION
FROM MaterialLocations ML
INNER JOIN @materialScan S ON S.BARCODE = ML.BARCODE
SELECT @@ROWCOUNT [ROWS_AFFECTED]

SELECT * FROM MaterialLocations

