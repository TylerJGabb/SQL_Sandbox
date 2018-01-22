DECLARE @startDate DATETIME
DECLARE @endDate DATETIME
SET @endDate = GETDATE()
SET @startDate = '01/01/2018'

SELECT 
	J.Job_Number,
	SUM(ISNULL(Weight,0) - ISNULL(Pallet_Weight,0)) 'Waste (lbs)',
	100 * (SUM(ISNULL(Weight,0) - ISNULL(Pallet_Weight,0))) / (U.Pounds) [Percent Waste],
	U.Pounds,
	J.Description,
	J.Status
FROM Bales B
INNER JOIN Jobs J ON J.Job_Number = CONVERT(INT,SUBSTRING(Waste_Type,1,CHARINDEX(' ',Waste_Type) - 1))
LEFT OUTER JOIN (SELECT Job_Number, SUM(Pounds) Pounds FROM MaterialsUsed GROUP BY Job_Number) U ON U.Job_Number = J.Job_Number
WHERE (Waste_Type LIKE '%[0-9] press%' OR Waste_Type LIKE '%[0-9] slit%')
AND B.Created_On BETWEEN @startDate AND @endDate
GROUP BY Waste_Type, J.Job_Number, J.Description, U.Pounds, J.Status
Order by 3 Desc

