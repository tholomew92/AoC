import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

def check_neighbours(rolls, hpos, wpos):
    hits = 0
    offsets = [(-1, -1), (-1, 0), (-1, 1), 
               (0, -1),         (0, 1), 
               (1, -1), (1, 0), (1, 1)]

    for dh, dw in offsets:
        h, w = hpos + dh, wpos + dw
        if 0 <= h < height and 0 <= w < width:
            if rolls[h][w] == '@':
                hits += 1
                if hits == 4:
                    return False
    return True

def remove_rolls(rollmap):
    hitpos = []
    for h in range(height):
        for w in range(width):
            if rollmap[h][w] == '@':
                if check_neighbours(rollmap, h, w):
                    hitpos.append((h,w))
    return hitpos

data = [line.rstrip() for line in open(inputfilepath)]


width = len(data[0])
height = len(data)

first = True
rolls = [list(s) for s in data]

p1 = 0
p2 = 0

loops = 0

while True:
    hits = remove_rolls(rolls)
    if len(hits) == 0:
        break
    if first:
        p1 = len(hits)
        first = False
    p2 += len(hits)
    for h, w in hits:
        rolls[h][w] = '.'


end_time = time.time() - start_time

print(f"Part one: {p1}")
print(f"Part two: {p2}")
print(f"Time taken: {end_time:3f} s")