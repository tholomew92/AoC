import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]

tdict = {}
prevposmap = {}
positions = []
pos = 0,0
startpos = 0,0
startdir = ''
dirs = ['^', '>', 'v', '<']
xrange = range(0, len(data))
yrange= range(0, len(data[0]))

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
            x -= 1
        case '>':
            y += 1
        case 'v':
            x += 1
        case '<':
            y -= 1
    return x, y

def walkthrough(pos, dir, data, tdict):
    while True:
        if pos not in tdict:
            tdict[pos] = []
        if dir in tdict[pos]:
            return 1
        tdict[pos].append(dir)
        nextx, nexty = nextpos(pos, dir)
        if nextx not in xrange or nexty not in yrange:
            break
        nextobj = data[nextx][nexty]
        while nextobj == '#':
            dir = turn(dir)
            nextx, nexty = nextpos(pos, dir)
            nextobj = data[nextx][nexty]  
        pos = nextx, nexty
    return 0

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

walkthrough(startpos, startdir, data,  tdict)

partone_time = time.time() - start_time

positions = tdict.keys()

parttwo = 0
prevpos = startpos
prevdir = ''
for p in positions:
    if p == startpos:
        continue
    dir = getdir(p, prevpos)
    copy = [list(row) for row in data]
    x,y = p
    copy[x][y] = '#'
    local_tdict = {}
    loop = walkthrough(prevpos, dir, copy, {})
    parttwo += loop
    prevpos = p
    
print(f"Part One: {len(positions)} and took {partone_time*1000:2f} ms")
print(f"Part Two: {parttwo} and took {time.time() - start_time:2f} sec")