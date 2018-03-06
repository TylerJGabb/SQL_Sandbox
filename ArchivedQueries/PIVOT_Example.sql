SELECT
	*
FROM
(
	SELECT 
		IncomeDay [Day],
		Count(IncomeDay) [Quant]
	FROM DailyIncome
	GROUP BY IncomeDay
) SRC
PIVOT
(
	Max(Quant)
	FOR Day IN ([MON],[TUE],[WED],[THU],[FRI],[SAT],[SUN])
) PIV 



