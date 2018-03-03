DECLARE @materialScan AS InventoryScanTable
--INSERT INTO @materialScan (PREFIX,BARCODE,LOCATION) VALUES ('P',10001,'TEST1')
--INSERT INTO @materialScan (PREFIX,BARCODE,LOCATION) VALUES ('P',10002,'TEST1')
--INSERT INTO @materialScan (PREFIX,BARCODE,LOCATION) VALUES ('R',10003,'TEST1')
INSERT INTO @materialScan (PREFIX,BARCODE,LOCATION) VALUES ('R',10004,'TEST2')
SELECT * FROM @materialScan




EXEC inventory_UploadMaterialScanTable @materialScan

SELECT * FROM MaterialLocations