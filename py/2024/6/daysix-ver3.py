import os
import time
from concurrent.futures import ProcessPoolExecutor 

    
start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]

tdict = {}
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


if __name__ == "__main__":

        
    walkthrough(startpos, startdir, data,  tdict)
    
    positions = []
    for k in tdict:
        positions.append(k)
    partone_time = time.time() - start_time

    print(f"Part One: {len(positions)}. It took {partone_time*1000:.3f} ms")


    for x in range(1,21):
        parttwo_start = time.time()
        parttwo = 0
        with ProcessPoolExecutor(max_workers=x) as executor:
            results = list(executor.map(parttwo_walktrough, positions, [startpos]*len(positions), [startdir]*len(positions), [data]*len(positions)))


        parttwo = sum(results)
        parttwo_stop = time.time() - parttwo_start
        print(f"Part Two: {parttwo}. It took {parttwo_stop:.3f} sec with {x} work(er)")
    