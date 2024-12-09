import os
import time
from concurrent.futures import ProcessPoolExecutor 

    
start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]

tdict = {}
prevposdict = {}
startpos = 0,0
startdir = ''
prevpos = 0,0
dirs = ['^', '>', 'v', '<']
xrange = range(0, len(data))
yrange= range(0, len(data[0]))

for x in range(len(data)):
    line = data[x]
    found = False 
    for y in range(len(line)):
        if line[y] in dirs:
            startdir = line[y]
            startpos = (x,y)
            found = True
            break
    if found:
        break

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

def parttwo_walktrough(p, startpos, data):
    print(f"{p} vs {startpos}")
    if p == startpos:
        return 0
    start = prevposdict[p]
    dir = getdir(p, start)
    if p in prevposdict:
        start = prevposdict[p]
        dir = getdir(p, start)
    copy = [list(row) for row in data]
    x,y = p
    copy[x][y] = '#'
    local_tdict = {}
    loop = walkthrough(start, dir, copy, local_tdict)
    return loop


if __name__ == "__main__":
        
    walkthrough(startpos, startdir, data,  tdict)
    
    positions = []
    prevpos = (-1,-1)
    for k in tdict:
        positions.append(k)
        if k == startpos:
            prevpos = startpos
        prevposdict[k] = prevpos
        print(f"Added {prevpos} as prevpos to {k}")
        if len(prevposdict) > 50:
            break
        prevpos = k
    
    partone_time = time.time() - start_time

    print(f"Part One: {len(positions)}. It took {partone_time*1000:.3f} ms")
    for x in range(1,21):
        parttwo_start = time.time()
        parttwo = 0
        #with ProcessPoolExecutor(max_workers=x) as executor:
            #results = list(executor.map(parttwo_walktrough, positions, [startpos]*len(positions), [data]*len(positions)))


        parttwo = sum(results)
        parttwo_stop = time.time() - parttwo_start
        print(f"Part Two: {parttwo}. It took {parttwo_stop:.3f} sec with {x} work(er)")
    