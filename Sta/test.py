amount = 100
percent = 1.03
counter = 0
while amount < 200:
	counter = counter + 1
	amount = amount * percent
	print(str(counter) + " " + str(amount))