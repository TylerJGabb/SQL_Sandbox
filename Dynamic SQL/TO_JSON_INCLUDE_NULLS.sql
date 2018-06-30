USE AdventureWorks2014
SELECT TOP 100
	FirstName,
	MiddleName,
	LastName
FROM Person.Person
FOR JSON AUTO, INCLUDE_NULL_VALUES 
FOR JSON PATH, ROOT('Person.Address')
