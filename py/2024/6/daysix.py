import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]

positions = []
pos = 0,0
startpos = 0,0
dirs = ['^', '>', 'v', '<']
minx, maxx = 0, len(data) - 1
miny, maxy = 0, len(data[0]) - 1
dir = ''
startdir = ''

def getdir(currpos, prevpos):
    cx, cy = currpos
    px, py = prevpos
    if cx < px:
        return '^'
    elif cx > px:
        return 'v'
    elif cy < py:
        return '<'
    elif cy > py:
        return '>'
    return ''

def turn(currdir):
    index = dirs.index(currdir)
    if index == 3:
        return dirs[0]
    return dirs[index + 1]

def nextpos(currpos, currdir):
    x, y = currpos
    match currdir:
        case '^':
            x = x - 1
        case '>':
            y = y + 1
        case 'v':
            x = x + 1
        case '<':
            y = y - 1
    return x, y


for x in range(len(data)):
    line = data[x]
    found = False 
    for y in range(len(line)):
        if line[y] in dirs:
            dir = line[y]
            startdir = line[y]
            pos = (x,y)
            startpos = (x,y)
            found = True
            break
    if found:
        break

while True:
    if pos not in positions:
        positions.append(pos)
    nextx, nexty = nextpos(pos, dir)
    if nextx < minx or nextx > maxx or nexty < miny or nexty > maxy:
        break
    elif data[nextx][nexty] == '#':
        dir = turn(dir)
        nextx, nexty = nextpos(pos, dir)  
    pos = nextx, nexty

parttwo = 0

prevpos = startpos

for p in positions:
    if p == startpos:
        continue
    pos = prevpos
    dir = getdir(p, prevpos)
    copy = data.copy()
    x,y = p
    substr = copy[x][:y] + "#" + copy[x][y+1:]
    copy[x] = substr
    loop = False
    tdict = dict()
    while True:
        if pos not in tdict:
            tdict[pos] = []
        if dir in tdict[pos]:
            loop = True
            break
        tdict[pos].append(dir)
        nextx, nexty = nextpos(pos, dir)
        if nextx < minx or nextx > maxx or nexty < miny or nexty > maxy:
            break
        nextobj = copy[nextx][nexty]
        while nextobj == '#':
            dir = turn(dir)
            nextx, nexty = nextpos(pos, dir)
            nextobj = copy[nextx][nexty]  
        pos = nextx, nexty

    if loop:
        parttwo = parttwo + 1
    prevpos = p
    
end_time = time.time()
total_time = end_time - start_time
print(f"Part One: {len(positions)}")
print(f"Part Two: {parttwo}")
print(f"Time run: {total_time}")