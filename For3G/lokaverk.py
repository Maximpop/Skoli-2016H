"""import mysql.connector
from mysql.connector import errorcode

try:
	cnx = mysql.connector.connect(user='2410982069', password='pass.123', host='tsuts.tskoli.is', database='2410982069_pygame')
	db = cnx.cursor()

except Exception, e:
	print("error in connecting to the database")

def newScorer(name, score):
	query = ('CALL newWinner();')	
	db.execute(query, (name, str(score)))

def isTop(name, score):
	query = ('SELECT topScorer();')
	top = db.execute(query)
	print(query)
	print(top)
	if score > top:
		newScorer(name, score)
#def getTop()


isTop('Pall', 15)


db.commit()
db.close()
cnx.close()



"""

import mysql.connector
from mysql.connector import errorcode


global cnx
try:
	cnx = mysql.connector.connect(user='2410982069', password='pass.123', host="tsuts.tskoli.is", database='2410982069_pygame')
	print('success')
except Exception, e:
	if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
		print("Something is wrong with your user name or password")
	elif err.errno == errorcode.ER_BAD_DB_ERROR:
		print("Database does not exist")
	else:
		print(err)
else:
	cnx.close()


cursor = cnx.cursor()

query = 'call newWinner("Pall", 500);'
cursor.execute(query)
cursor.close()
cnx.close()