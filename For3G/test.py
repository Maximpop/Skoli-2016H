import mysql.connector
from mysql.connector import errorcode

class connection:
    def CheckConnection():
        global cnx
        try:
            cnx
        except mysql.connector.Error as err:
            if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
                print("Something is wrong with your user name or password")
            elif err.errno == errorcode.ER_BAD_DB_ERROR:
                print("Database does not exist")
            else:
                print(err)
        else:
            cnx.close()

    def newScorer(name, score):

        cursor = cnx.cursor()
        query = ('Call newWinner("Alex", 208);')
        cursor.execute(query)
        cursor.close()
        cnx.close()

    def isTop(name, score):
        query = ('SELECT topScorer();')
        top = db.execute(query)
        if score > top:
            newScorer(name, score)
            print('congratz')
        else:
        	print('sorry')





connection.isTop('Pall', 54)
