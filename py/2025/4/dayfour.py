import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

def check_neighbours(hpos, wpos):
    hits = 0
    minh = maxh = minw = maxw = -1
    if hpos == 0:
        minh = 0
    else:
        minh = height - 1
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
                if data[h][w] == '@':             
                    hits += 1
                    if hits > 2:
                        print(f"pos {hpos},{wpos}: {h},{w}")
    return hits

data = [line.rstrip() for line in open(testfilepath)]

width = len(data[0])
height = len(data)

p1 = 0
for h in range(height):
    for w in range(width):
        if data[h][w] == '@':
            if check_neighbours(h, w) < 4:
                p1 += 1

print(p1)