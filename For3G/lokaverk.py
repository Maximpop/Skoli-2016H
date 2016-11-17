import mysql.connector

cnx = mysql.connector.connect(user='2410982069', password='Svoluhofdi2', host='tsuts.tskoli.is', database='2410982069_pygame')
db = cnx.cursor()


def newScorer(name, score):
	query = ('call newWinner(%s,%s,)')	
	result = db.execute(query, (name, str(score)))

def isTop(name, score):
	query = ('SELECT topScorer()')
	top = db.execute(query)
	if score > top:
		newScorer(name, score)

isTop('Palli', 15)



#muna ad setja i lokinn
db.close()
cnx.close()



"""
Hvada info viljum vid i db?
name(sem notandi slar inn ef hann vinnur)
stigin sem hann fekk
datetime

hvad meira?

"""