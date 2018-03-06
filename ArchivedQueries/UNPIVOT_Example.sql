CREATE TABLE #combos
(
	PMS1 NVARCHAR(32),
	PMS2 NVARCHAR(32),
	PMS3 NVARCHAR(32),
	PMS4 NVARCHAR(32)
)


INSERT INTO #combos VALUES ('FIRST','SECOND','THIRD','FOURTH')
--INSERT INTO #combos VALUES ('5','6','7','8') --trying to figure out what behavior this leads to
SELECT * FROM #combos


SELECT
	ColName,
	PMS_NAME
FROM
(SELECT * From #combos) SRC
UNPIVOT
(
	PMS_NAME
	FOR ColName IN ([PMS1],[PMS2],[PMS3],[PMS4])
) PIV



DROP TABLE #combos