import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(testfilepath)]

positions = []
trailheads = []
completepaths = []
dirs = ((-1, 0),(1, 0),(0, -1),(0, 1))
xrange = range(0, len(data))
yrange = range(0, len(data[0]))
scores = dict()

def find_path(pos, path):
    x, y = pos
    curpos = int(data[x][y])
    if curpos == 9:
        return path
    paths = []
    for d in dirs:
        nx, ny = d
        if x+nx not in xrange or y+ny not in yrange:
            continue
        nexpos = int(data[x+nx][y+ny])
        npos = (x+nx,y+ny)
        if nexpos - curpos == 1:
            paths += find_path(npos, path + [npos])
    return paths

for x in xrange:
    for y in yrange:
        if data[x][y] == '0':
            trailheads.append((x,y))

for trailhead in trailheads:
    print(trailhead)
    score = 0
    paths = find_path(trailhead, [])
    print(paths)

print(data[1][2])