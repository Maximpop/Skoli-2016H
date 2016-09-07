from random import randint

def dicet():
	return(randint(1,6))

def reThrow():
	placeholder = []
	for x in xrange(1,6):
		placeholder.append(dicet)

def show(listi):
	for x in xrange(0,5):
		print(str(listi[x]))

listi=[]
comptua = []
decision = "no"


for x in xrange(1,6):
	listi.append(dicet())
	comptua.append(dicet())

for x in xrange(1,4):
	show(listi)
	decision = raw_input("Do you want to throw again y/n ")
	if decision == "n":
		break
	else:
		reThrow()

print("Computer dice")
show(comptua)

print("Your dice")
show(listi)

if sum(comptua)>sum(listi):
	print("You lose")
elif sum(listi) > sum(comptua):
	print("You win")
else:
	print("Draws")