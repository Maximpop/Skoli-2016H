import pygame

# constants
LEFT_BUTTON = 1
WINDOW_SIZE = (640, 480)
BLACK = (0, 0, 0)
BLUE = (0, 0, 255)
RED = (255, 0, 0)

running = True

window = pygame.display.set_mode(WINDOW_SIZE)
window.fill(BLACK)

# To show more than one rectangle and have the mouse interact
# with all of them, we best put them into a list and iterate
# through them to have them drawn on the screen.
rect_list = []

rect_list.append(pygame.Rect(30, 30, 100, 100))
rect_list.append(pygame.Rect(160, 30, 100, 100))
rect_list.append(pygame.Rect(290, 30, 100, 100))
rect_list.append(pygame.Rect(420, 30, 100, 100))

# now we need to paint the box on the screen and give it some (initial) color
# in the process(here: red)
for box in rect_list:
    pygame.draw.rect(window, RED, box)

while running:
    event = pygame.event.poll()
    if event.type == pygame.QUIT:
        running = False
    elif event.type == pygame.MOUSEBUTTONDOWN and event.button == LEFT_BUTTON:
        # Now each time the user clicks on the left mouse button the program
        # runs through the list of rects and for each rect runs the collidepoint()
        # function. If so happens that the mouse is within one of them it is instantly
        # painted blue. The chosen colors for the rects is by no means a political statement :-)
        for box in rect_list:
            if box.collidepoint(event.pos):
                pygame.draw.rect(window, BLUE, box)

    pygame.display.flip()

