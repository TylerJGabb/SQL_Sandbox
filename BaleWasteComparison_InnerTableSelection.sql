DECLARE @startDate DATETIME
DECLARE @endDate DATETIME
SET @endDate = GETDATE()
SET @startDate = '01/01/2018'

SELECT
	T1.*,
	Reported.*
FROM 
(SELECT 
	J.Job_Number,
	SUM(ISNULL(Weight,0) - ISNULL(Pallet_Weight,0)) 'Waste (lbs)',
	100 * (SUM(ISNULL(Weight,0) - ISNULL(Pallet_Weight,0))) / (U.Pounds) [Percent Waste],
	U.Pounds,
	J.Description,
	J.Status
FROM Bales B
LEFT OUTER JOIN Jobs J ON J.Job_Number = CONVERT(INT,SUBSTRING(Waste_Type,1,CHARINDEX(' ',Waste_Type) - 1))
LEFT OUTER JOIN (SELECT Job_Number, SUM(Pounds) Pounds FROM MaterialsUsed GROUP BY Job_Number) U ON U.Job_Number = J.Job_Number
WHERE (Waste_Type LIKE '%[0-9] press%' OR Waste_Type LIKE '%[0-9] slit%')
AND B.Created_On BETWEEN @startDate AND @endDate
GROUP BY Waste_Type, J.Job_Number, J.Description, U.Pounds, J.Status) T1
INNER JOIN 
(SELECT
   Job_Number,
   Sum(isnull(W.Press_Waste_Reported,0)) as 'Press Waste',
   Sum(isnull(W.Slitter_Waste_Reported,0)) as 'Slitter Waste',
   Sum(W.Roll_Weight) 'Roll Weight'
FROM WipRoll W
inner join RunCombos RC on W.Combo_Number = RC.Record_Number and RC.Description like '%niagara%'
inner join (Select Distinct Combo_Number, Job_Number From ComboItems) CI on RC.Record_Number = CI.Combo_Number
WHERE IsNull(W.Printed,GETDATE()) BETWEEN @startDate and @endDate
Group by Job_Number ) Reported ON Reported.Job_Number = T1.Job_Number
ORDER BY 3 DESC