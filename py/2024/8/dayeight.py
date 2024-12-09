import os
import time
import math

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]

nodes = dict()
xrange = range(0, len(data))
yrange = range(0, len(data[0]))
antinodes = dict()
partone = set()
parttwo = set()

def get_anti_nodes(n1, n2):
    n1x, n1y = n1
    n2x, n2y = n2
    xdif = n2x - n1x
    ydif = n2y - n1y

    an1x, an1y = n1x - xdif, n1y - ydif
    an2x, an2y = n2x + xdif, n2y + ydif

    # Part One

    if an1x in xrange and an1y in yrange:
        if (an1x, an1y) not in partone:
            partone.add((an1x, an1y))
    if an2x in xrange and an2y in yrange:
        if (an2x, an2y) not in partone:
            partone.add((an2x, an2y))

    # Part Two

    gcd = math.gcd(abs(xdif), abs(ydif))
    x_step = xdif // gcd
    y_step = ydif // gcd
    x, y = n1x, n1y
    while (x, y) != n2:
        if (x,y) not in parttwo:
            parttwo.add((x,y))
        x += x_step
        y += y_step
        
    x, y = n1x, n1y
    while x in xrange and y in yrange:
        if(x, y) not in parttwo:
            partone.add((x,y))
        x -= x_step
        y -= y_step
    x, y = n2x, n2y
    while x in xrange and y in yrange:
        if (x, y) not in parttwo:
            parttwo.add((x,y))
        x += x_step
        y += y_step


for x in range(len(data)):
    for y in range(len(data[x])):
        node = data[x][y]
        if node != '.':
            if node not in nodes:
                nodes[node] = []
            nodes[node].append((x,y))         


for node in nodes:
    nodelist = nodes[node]
    for x in range(len(nodelist) - 1):
        for y in range(x + 1, len(nodelist)):
            n1, n2 = nodelist[x], nodelist[y]
            get_anti_nodes(n1, n2)

p1 = 0
for x in antinodes:
    print(f"{x}: {antinodes[x]}")
    p1 += len(antinodes[x])

parttwo.update(partone)
end_time = time.time() - start_time
print(len(partone))
print(len(parttwo))
print(f"Time taken is {end_time*1000:2f} ms")