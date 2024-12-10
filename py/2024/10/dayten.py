import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]

positions = []
trailheads = []
completepaths = []
dirs = ((-1, 0),(1, 0),(0, -1),(0, 1))
xrange = range(0, len(data))
yrange = range(0, len(data[0]))
partonescores = dict()
parttwoscores = dict()

def find_path(pos, path):
    x, y = pos
    curpos = int(data[x][y])
    if curpos == 9:
        return path
    paths = []
    for d in dirs:
        dx, dy = d
        if x+dx not in xrange or y+dy not in yrange:
            continue
        nexpos = int(data[x+dx][y+dy])
        npos = (x+dx,y+dy)
        if nexpos - curpos == 1:
            paths += find_path(npos, path + [npos])
    return paths

for x in xrange:
    for y in yrange:
        if data[x][y] == '0':
            trailheads.append((x,y))

for trailhead in trailheads:
    p1score = 0
    p2score = 0
    paths = find_path(trailhead, [])
    pset = set(paths)
    for path in pset:
        x, y = path
        if data[x][y] == '9':
            p1score += 1
    partonescores[trailhead] = p1score
    for path in paths:
        x, y = path
        if data[x][y] == '9':
            p2score += 1
    parttwoscores[trailhead] = p2score

partone = 0
for score in partonescores:
    partone += partonescores[score]
parttwo = 0
for score in parttwoscores:
    parttwo += parttwoscores[score]
print(f"Part One: {partone} ms")
print(f"Part Two: {parttwo} ms")
print(f"Time taken is {time.time() - start_time}")