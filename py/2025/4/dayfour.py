import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

def check_neighbours(rolls, hpos, wpos):
    hits = 0
    minh = maxh = minw = maxw = -1
    if hpos == 0:
        minh = 0
    else:
        minh = hpos - 1
    if hpos == height - 1:
        maxh = hpos + 1
    else:
        maxh = hpos + 2
    if wpos == 0:
        minw = 0
    else:
        minw = wpos - 1
    if wpos == width - 1:
        maxw = wpos + 1
    else:
        maxw = wpos + 2
    for h in range(minh , maxh):
        for w in range(minw, maxw):
            if h == hpos and w == wpos:
                continue
            else:
                
                if rolls[h][w] == '@':
                    hits += 1
    return hits

def remove_rolls(rollmap):
    hitpos = []
    for h in range(height):
        for w in range(width):
            if rollmap[h][w] == '@':
                if check_neighbours(rollmap, h, w) < 4:
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
    if first:
        p1 = len(hits)
        first = False
    if len(hits) == 0:
        break
    p2 += len(hits)
    for h, w in hits:
        rolls[h][w] = '.'


end_time = time.time() - start_time

print(f"Part one: {p1}")
print(f"Part two: {p2}")
print(f"Time taken: {end_time:3f} s")