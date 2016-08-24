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


for x in xrange(1,6):
	listi.append(dicet())
	comptua.append(dicet())

for x in xrange(1,4):
	show(listi)
	decision = input("Do you want to throw again y/n");
	if decision =='n':
		break
	elif decision == 'y':
		reThrow()
	else:
		print("invalid input")

print("Computer dice")
show(comptua)

print("Your dice")
show(listi)