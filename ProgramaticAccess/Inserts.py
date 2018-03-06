import pymssql
import string
import random
chars = string.ascii_letters + '1234567890'

def generate_rando_str():
    ret = ''
    for i in range(random.randint(10,30)):
        ret += random.choice(chars)
    return ret if random.choice([0,1,1,1]) else ''

def do_inserts(N):
    con = pymssql.connect(server='serverName.database.windows.net',user="user@serverName", password="password", database="catalog")
    cursor = con.cursor()
    formatter = ','.join(['{' + str(i) + '}' for i in range(10)])
    for i in range(N):
        rando_strs = ["'" + generate_rando_str() + "'" for i in range(10)]
        insert = formatter.format(*rando_strs)
        SQL = 'INSERT INTO LearningPivoting VALUES ({0})'.format(insert)
        cursor.execute(SQL)
    con.commit()
    con.close()
        
