import os
here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]

positiondict = dict()
pos = 0,0
startpos = 0,0
dirs = ['^', '>', 'v', '<']
xrange = range(0, len(data))
yrange= range(0, len(data[0]))
startdir = ''


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

def walkthrough(pos, dir, data, first):
    debug = False
    if pos == (23,44):
        debug = True
    tdict = dict()
    while True:
        if pos not in tdict:
            tdict[pos] = []
        if dir in tdict[pos]:
            return -1
        tdict[pos].append(dir)
        if debug:
            print(f"{pos}: {tdict[pos]}")
        nextx, nexty = nextpos(pos, dir)
        if nextx not in xrange or nexty not in yrange:
            if first:
                return tdict
            return 0
        nextobj = data[nextx][nexty]
        while nextobj == '#':
            dir = turn(dir)
            nextx, nexty = nextpos(pos, dir)
            nextobj = data[nextx][nexty]  
        pos = nextx, nexty

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

positions = walkthrough(startpos, startdir, data, True)

print(f"Part One: {len(positions)}")
parttwo = 0


for p in positions:
    if p == startpos:
        continue
    print(p)
    dir = startdir
    copy = data.copy()
    x,y = p
    substr = copy[x][:y] + "#" + copy[x][y+1:]
    copy[x] = substr
    loop = walkthrough(p, dir, copy, False)
    #print(loop)
    if loop == -1:
        parttwo = parttwo + 1

print(parttwo)