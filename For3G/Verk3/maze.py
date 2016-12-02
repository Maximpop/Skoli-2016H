import pygame

# This demo is based in most parts on a collision demo at the original PyGqme website.
# http://www.pygame.org/project-Rect+Collision+Response-1061-.html
# accessed on 12-10-2016
# Information about the original author not found(broken link ?)


# Class for the Player(the orange dude)
class Player(object):

    def __init__(self):
        self.rect = pygame.Rect(32, 32, 20,20)

    def move(self, dx, dy):
        # Move each axis separately. Note that this checks for collisions both times.
        if dx != 0:
            self.move_single_axis(dx, 0)
        if dy != 0:
            self.move_single_axis(0, dy)

    def move_single_axis(self, dx, dy):
        # Move the rectangle
        self.rect.x += dx
        self.rect.y += dy
        # If you collide with a wall, move out based on velocity
        for brick in bricks:
            if self.rect.colliderect(brick.rect):
                if dx > 0: # Moving right; Hit the left side of the wall
                    self.rect.right = brick.rect.left
                if dx < 0: # Moving left; Hit the right side of the wall
                    self.rect.left = brick.rect.right
                if dy > 0: # Moving down; Hit the top side of the wall
                    self.rect.bottom = brick.rect.top
                if dy < 0: # Moving up; Hit the bottom side of the wall
                    self.rect.top = brick.rect.bottom

# Nice class to hold a wall rectangle
class Brick(object):

    def __init__(self, pos):
        self.rect = pygame.Rect(pos[0], pos[1], 20,20)

# Initialize
pygame.init()

# Set up the display
pygame.display.set_caption("Maze")
screen = pygame.display.set_mode((640, 480))

clock = pygame.time.Clock()
bricks = list()     # List to hold the walls
points = list()     # List of points
mines = list()      # List of mines
player = Player()   # Create the player

# Holds the level layout in a list of strings.
maze = [
"WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW",
"W     E                        W",
"W         WWWWWW  E            W",
"W   WWWW       W               W",
"W   W        WWWW      M       W",
"W WWW  WWWW     M         E    W",
"W   W     W W                  W",
"W   W     W   WWW W            W",
"W   WWW WWW M W W              W",
"W     W   W   W W         E    W",
"WWW   W   WWWWW W              W",
"W W      WW                M   W",
"W W   WWWW   WWW    M          W",
"W     W    E   W               W",
"W                              W",
"W                              W",
"W                    E         W",
"W       E        M             W",
"W                              W",
"W                          M   W",
"W                M             W",
"W                              W",
"W       E                     LW",
"WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW"
]

# Parse the maze string above. W = wall, L = exit, M = mine E = point
x = 0
y = 0
for row in maze:
    for col in row:
        if col == "W":
            bricks.append(Brick((x, y)))
        if col == "E":
            points.append(pygame.Rect(x, y, 20, 20))
        if col == "M":
            mines.apppend(pygame.Rect(x, y, 20,20))
        if col == "L":
            magic_rect = pygame.Rect(x,y,20,20)
        x += 20
    y += 20
    x = 0

running = True

while running:
    clock.tick(60)

    for e in pygame.event.get():
        if e.type == pygame.QUIT:
            running = False
        if e.type == pygame.KEYDOWN and e.key == pygame.K_ESCAPE:
            running = False

    # Move the player if an arrow key is pressed
    key = pygame.key.get_pressed()
    if key[pygame.K_LEFT]:
        player.move(-2, 0)
    if key[pygame.K_RIGHT]:
        player.move(2, 0)
    if key[pygame.K_UP]:
        player.move(0, -2)
    if key[pygame.K_DOWN]:
        player.move(0, 2)

    # Check to see if the player has collided the magic box or the exit.
    if player.rect.colliderect(end_rect):
        raise SystemExit, 'You win!'
    if player.rect.colliderect(magic_rect):
        raise SystemExit, 'You lose!'

    # Draw the scene
    screen.fill((0, 0, 0))
    # every brick in the walls is drawn
    for brick in bricks:
        pygame.draw.rect(screen, (255, 255, 255), brick.rect)

    # the others are drawn
    pygame.draw.rect(screen, (255, 0, 0), end_rect)
    pygame.draw.rect(screen, (0, 0, 255), magic_rect)
    pygame.draw.rect(screen, (255, 200, 0), player.rect)

    pygame.display.flip()
