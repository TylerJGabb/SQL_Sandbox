--CREATE A TABLE TYPE
CREATE TYPE TestTable AS TABLE
(
	Integers INT,
	Strings NVARCHAR(32)
);
/*

This table is now available to be used as a parameter for a table valued function/proc
*/




