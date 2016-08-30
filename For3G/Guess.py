from random import randint

stig = 0
num = randint(1,100)
userNum=0
guessComplete = 0

for x in xrange(1,9):
	userNum= input("Select num no "+str(x)+" ")
	if userNum == num:
		guessComplete = x
		break
	elif userNum > num:
		print("you high bru")
	else:
		print("you loman")

if guessComplete == 1:
	stig = 1280
elif guessComplete == 2:
	stig = 640
elif guessComplete == 3:
	stig = 320
elif guessComplete == 4:
	stig = 160
elif guessComplete == 5:
	stig = 80
elif guessComplete == 6:
	stig = 40
elif guessComplete == 7:
	stig = 20
elif guessComplete == 8:
	stig = 10
else:
	stig = 0

if stig ==0:
	print("You lost")
else:
	print("You got "+ str(stig))	