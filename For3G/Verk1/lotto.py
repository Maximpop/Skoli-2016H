from random import randint


compList = list()
userList = list()
bonus = 0 #cuz I can

for x in xrange(0,5):
	compList.append(randint(1,9))

for x in xrange(1,6):
	userList.append(input("select num " + str(x)+" "))

userList.append(input("Select your bonus number "))

num = 0
for x in xrange(0,4):
	for a in xrange(0,4):
		if compList[x] == userList[a]:
			num = num + 1
if num ==0:
	print("lol u fail man. No bonus 4 u")

else:
	print("You have " + str(num)+ " numbers correct")
	if compList[5] == userList[5]:
		print("You have the bonus number correct")
	else:
		print("You have the bonus number wrong")