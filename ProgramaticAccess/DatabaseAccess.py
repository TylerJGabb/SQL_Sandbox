import pymssql
class DatabaseAccess:
    def __init__(self,server,user,password,database):
        if(not '@' in user):
            raise ValueError("Argument provided for `user` in wrong format\n"+
            "Must be in format user@server\n" + 
            "https://docs.microsoft.com/en-us/sql/connect/python/pymssql/step-3-proof-of-concept-connecting-to-sql-using-pymssql")
        self.conn = pymssql.connect(
            server=server,
            user=user,
            password=password,
            database=database)

    def execute_cursor(self,sqlCommand):
        cursor = self.conn.cursor()
        cursor.execute(sqlCommand)
        return cursor

    def execute_rows(self,sqlCommand):
        cursor = self.conn.cursor()
        cursor.execute(sqlCommand)
        rows = [];
        row = cursor.fetchone()
        while row:
            rows.append(row)
            row = cursor.fetchone()
        return rows

    def execute_string(self,sqlCommand):






