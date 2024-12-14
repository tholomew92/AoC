import os
import time
import re

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = open(inputfilepath, 'r').read()

maxx = 101
maxy = 103
pattern = r'[-]?\d+'
matches = list(map(int, re.findall(pattern, data)))


robodict = dict()
id = 0
for i in range(0, len(matches), 4):
    startx, starty, vecx, vecy = matches[i], matches[i+1],matches[i+2], matches[i+3]
    robodict[id] = (startx, starty, vecx, vecy)
    id += 1

def walk_the_robos(loops, robodict):
    partone = 0
    for i in range(8006):
        for robo in robodict:
            #print(robo)
            x, y, xv, yv = robodict[robo]
            if x + xv < 0:
                x = maxx + x + xv
            elif x + xv >= maxx:
                x = x + xv - maxx
            else:
                x = x + xv
            if y + yv < 0:
                y = maxy + y + yv
            elif y + yv >= maxy:
                y = y + yv - maxy
            else:
                y = y + yv
            robodict[robo] = (x, y, xv, yv)
        if i == loops:
            partone = get_score(robodict)
        #input(f"Current loop {i}. Next?")
    
    print_tree(robodict)
    return partone

def print_tree(robodict):
    treelist = []
    for y in range(maxy):
        treelist.append([])
        for j in range(maxx):
            treelist[y].append('.')

    for robo in robodict:
        x,y,_,_ = robodict[robo]
        treelist[y][x] = '#'
    hitc = 0
    for l in treelist:
        str = ''.join(l)
        print(str)
        if str.count('#') > hitc:
            hitc = str.count('#')

    print(hitc)


def get_score(robodict):
    q1 = 0
    q2 = 0
    q3 = 0
    q4 = 0
    xlow = range(0, maxx // 2)
    xhigh = range(maxx - (maxx//2), maxx)
    ylow = range(0, maxy // 2)
    yhigh = range(maxy - (maxy//2), maxy)
    for robo in robodict:
        x, y, _, _ = robodict[robo]
        if x in xlow:
            if y in ylow:
                q1 += 1
            elif y in yhigh:
                q2 += 1
        elif x in xhigh:
            if y in ylow:
                q3 += 1
            elif y in yhigh:
                q4 += 1
    return q1 * q2 * q3 * q4

partone = walk_the_robos(100, robodict)


end_time = time.time() - start_time

print(partone)
print(end_time)