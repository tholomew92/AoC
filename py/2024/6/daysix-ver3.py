import os
import time
from concurrent.futures import ProcessPoolExecutor 

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]

tdict = {}
positions = []
pos = 0,0
startpos = 0,0
startdir = ''
dirs = ['^', '>', 'v', '<']
xrange = range(0, len(data))
yrange= range(0, len(data[0]))


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

def parttwo_walktrough(p, startpos, startdir, data):
    if p == startpos:
        return 0
    dir = startdir
    copy = [list(row) for row in data]
    x,y = p
    copy[x][y] = '#'
    local_tdict = {}
    loop = walkthrough(startpos, dir, copy, local_tdict)
    return loop

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

for k in tdict:
    positions.append(k)

parttwo = 0

if __name__ == "__main__":
    with ProcessPoolExecutor(max_workers=8) as executor:
        results = list(executor.map(parttwo_walktrough, positions, [startpos]*len(positions), [startdir]*len(positions), [data]*len(positions)))


    parttwo = sum(results)

    end_time = time.time()
    total_time = end_time - start_time
    print(f"Part One: {len(positions)}")
    print(f"Part Two: {parttwo}")
    print(f"Time run: {total_time}")