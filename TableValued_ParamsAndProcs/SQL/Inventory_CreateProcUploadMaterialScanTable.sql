ALTER PROC inventory_UploadMaterialScanTable(@table AS InventoryScanTable READONLY) AS BEGIN

UPDATE 
	MaterialLocations
SET 
	MaterialLocations.LOCATION = S.LOCATION
FROM 
	MaterialLocations ML
	INNER JOIN @table S ON S.BARCODE = ML.BARCODE

END