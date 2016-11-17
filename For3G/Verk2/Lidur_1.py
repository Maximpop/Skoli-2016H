import pygame
import random
pygame.init()
font = pygame.font.SysFont('Arial', 25)


def findWinner():
	going = True
	if sum(comp_dice) > sum(user_dice):
		print("computer wins")
	elif sum(comp_dice) < sum(user_dice):
		print("You win")
	else:
		print("draw")
	
	while going == True:
		pygame.display.update()
		event = pygame.event.poll()	

		x=10
		y=10

		for a in xrange(0, len(comp_dice)):
			screen.blit(dice[comp_dice[a]-1], (x,y))
			x = x + dice_len
		
		y = y+dice_height
		x = 10


		for a in xrange(0,len(user_dice)):
			screen.blit(dice[user_dice[a]-1], (x,y))
			x = x + dice_len
		if event.type == pygame.QUIT:
			going = False

def throwOne():
	user_dice[4] = random.randint(1,6)
	findWinner()

def throwAll():
	for x in xrange(0,5):
		user_dice = list()
		user_dice.append(random.randint(1,6))
	findWinner()










width = 550
height = 350

white = (255,255,255)
black = (0,0,0)
blue = (0,0,255)
red = (255,0,0)

LEFT_BUTTON = 1

dice1 = pygame.image.load("Teningar/sd1.png")
dice2 = pygame.image.load("Teningar/sd2.png")
dice3 = pygame.image.load("Teningar/sd3.png")
dice4 = pygame.image.load("Teningar/sd4.png")
dice5 = pygame.image.load("Teningar/sd5.png")
dice6 = pygame.image.load("Teningar/sd6.png")

dice = [dice1,dice2,dice3,dice4,dice5,dice6]

dice_len, dice_height = dice1.get_rect().size
screen = pygame.display.set_mode((width,height)) 

pygame.display.set_caption("plato el plomo?")

comp_dice = list()
user_dice = list()

for x in xrange(0,5):
	comp_dice.append(random.randint(1,6))
	user_dice.append(random.randint(1,6))

rectA = pygame.Rect(75, 250, 150, 50)
rectB = pygame.Rect(250, 250, 150, 50)

running = True
done = False
while running == True:
	x = 10
	y = 10
	event = pygame.event.poll()	

	if event.type == pygame.MOUSEBUTTONDOWN and event.button == LEFT_BUTTON:
		if rectA.collidepoint(event.pos):
			throwOne()
			running = False

		if rectB.collidepoint(event.pos):
			throwAll()
			running = False

	elif event.type == pygame.QUIT:
		running = False
		break


	screen.fill(white)
		
	x = 10
	y = 10

	pygame.draw.rect(screen, red, rectA)
	screen.blit(font.render('Throw one!', True, (0,0,0)), (75, 250))
	pygame.draw.rect(screen, red, rectB)
	screen.blit(font.render('Throw all!', True, (0,0,0)), (250, 250))


	for a in xrange(0, len(comp_dice)):
		screen.blit(dice[comp_dice[a]-1], (x,y))
		x = x + dice_len
		
	y = y+dice_height
	x = 10

	for a in xrange(0,len(user_dice)-1):
		screen.blit(dice[user_dice[a]-1], (x,y))
		x = x + dice_len

	screen.blit(dice[random.randint(0,5)], (x,y))

	
	pygame.display.update()


		

pygame.quit()