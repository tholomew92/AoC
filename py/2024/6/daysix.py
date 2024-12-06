import os
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
dictt = dict()


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
    pos = startpos
    dir = startdir
    copy = data.copy()
    turns = []
    loop = False
    while True:
        nextx, nexty = nextpos(pos, dir)
        if nextx < minx or nextx > maxx or nexty < miny or nexty > maxy:
            break
        elif (nextx, nexty) == p or copy[nextx][nexty] == '#':
            #print(pos)
            turns.append((pos, dir))
            #print(len(turns))
            dir = turn(dir)
            nextx, nexty = nextpos(pos, dir)  
        pos = nextx, nexty

        if (pos,dir) in turns:
            loop = True
            break

    if loop:
        #print(p)
        parttwo = parttwo + 1
    prevpos = p

print(len(positions))
print(parttwo)