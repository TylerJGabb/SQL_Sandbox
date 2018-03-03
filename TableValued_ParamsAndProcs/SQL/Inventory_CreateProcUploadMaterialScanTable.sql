ALTER PROC inventory_UploadMaterialScanTable(@table AS InventoryScanTable READONLY) AS BEGIN
--https://stackoverflow.com/questions/2334712/how-do-i-update-from-a-select-in-sql-server
UPDATE 
	ML
SET 
	ML.LOCATION = S.LOCATION
FROM 
	MaterialLocations ML
	INNER JOIN @table S ON S.BARCODE = ML.BARCODE

END