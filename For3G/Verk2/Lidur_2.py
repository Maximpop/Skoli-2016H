import pygame
import random
pygame.init()
font = pygame.font.SysFont('Arial', 15)


width = 400
height = 200
white = (255,255,255)
red = (255,0,0)
blue = (0,0,255)
black = (0,0,0)
color = 0
LEFT_BUTTON = 1
rects = list()
rect_colors = list()
screen = pygame.display.set_mode((width, height))

x = 10
y = 10
size = 30

for a in xrange(0,7):
	rects.append(pygame.Rect(x,y,size,size))
	x = x + (size+5)

x = 10
y = 45
for a in xrange(0,7):
	rects.append(pygame.Rect(x,y,size,size))
	x = x + (size+5)


catsHit = 0
miceHit = 0
nullHit = 0

#red = cat
#blue = mouse
running = True
while running:
	screen.fill(white)
	event = pygame.event.poll()

	screen.blit(font.render('red = cat. So dont click them', True, (0,0,0)), (10, 80))
	screen.blit(font.render('blue = mouse. So click that fucker', True, (0,0,0,)), (10, 95))
	screen.blit(font.render('purple = Shouldnt be happening. Dont know about that one', True, (0,0,0)), (10, 110))

	if event.type == pygame.QUIT:
		print('Mice hit = ' + str(miceHit))
		print('Cats hit = ' + str(catsHit))
		print('Other hit= ' + str(nullHit))
		running = False
	elif event.type == pygame.MOUSEBUTTONDOWN and event.button == LEFT_BUTTON:
		for x in xrange(0,len(rects)):
			if rects[x].collidepoint(event.pos):
				if rect_colors[x] == 'Red':
					catsHit = catsHit = 1
				elif rect_colors[x] == 'Blue':
					miceHit = miceHit + 1
				else:
					nullHit = nullHit + 1
			#Stilla hvad notatdinn smellir a

	rect_colors = list()
	
	for x in xrange(0,len(rects)):
		color = random.randint(1,3)
		if color == 2:
			pygame.draw.rect(screen, red, rects[x])
			rect_colors.append('Red')
		elif color == 1:
			pygame.draw.rect(screen, blue, rects[x])
			rect_colors.append('Blue')
		else:
			rect_colors.append('None')


	pygame.display.update()