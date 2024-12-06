import os
here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]

positions = []
pos = 0,0
dirs = ['^', '>', 'v', '<']
minx, maxx = 0, len(data) - 1
miny, maxy = 0, len(data[0]) - 1
dir = ''
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
            pos = (x,y)
            found = True
            break
    if found:
        break
print(pos)
print(dir)
max = 500
start = 0
while start < max:
    #print(pos)
    if pos not in positions:
        positions.append(pos)
    nextx, nexty = nextpos(pos, dir)
    if nextx < minx or nextx > maxx or nexty < miny or nexty > maxy or data[nextx][nexty] == '#':
        dir = turn(dir)
        nextx, nexty = nextpos(pos, dir)    
        print(f"Turning from {pos} to ({nextx}, {nexty}), maxx is {maxx} and maxy is {maxy} nextpos has {data[nextx][nexty]}")
        if nextx < minx or nextx > maxx or nexty < miny or nexty > maxy or data[nextx][nexty] == '#':
            break
    
    pos = nextx, nexty
    start = start + 1

print(len(positions))