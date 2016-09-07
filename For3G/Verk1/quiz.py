Material = []
points = 0
"""
item 1 = question 1
items 2-3 = possible answers to question 1
item 4 = the answer to question 1
item 5 = question 2
items 6-8 = possible answers to question 2
item 9 = the answer to question 2
and so on"""

Material.append("1. Hvad er lengsta a landsins?")
Material.append("1. Olfusa")
Material.append("2. Thjorsa")
Material.append("3. Spraenan hja Konna")
Material.append("2")

#input("select num " + str(x)+" ")
for x in xrange(0,len(Material)):
	if len(Material[x]) == 1:
		answer = int(input('veldu thitt svar: '))
		if answer == int(Material[x]):
			points = points + 1
			print("Rett")
		else:
			print("Rangt")
	else:
		print(Material[x])

print("Game over")
print("points: "+ (str(points)))
