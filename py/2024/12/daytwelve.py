import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(testfilepath)]

positions = []
dirs = ((-1, 0),(0, -1),(1, 0),(0, 1))
xrange = range(0, len(data))
yrange = range(0, len(data[0]))
partonescores = dict()
parttwoscores = dict()

def find_plot(pos, plot):
    x,y = pos
    if pos not in plot:
        plot.add(pos)
    curpos = data[x][y]
    for d in dirs:
        dx, dy = d
        if x+dx not in xrange or y+dy not in yrange:
            continue
        nexpos = data[x+dx][y+dy]
        npos = (x+dx,y+dy)
        if nexpos == curpos and npos not in plot:
            plot.update(find_plot(npos, plot))

    return plot

def find_edges(plot):
    count = 0
    for pos in plot:
        x, y = pos
        for d in dirs:
            dx, dy = d
            if dx + x in xrange and dy + y in yrange:
                if data[x][y] != data[dx+x][dy+y]:
                    count += 1
            else:
                count += 1
    return count

def going_up(pos):
    dir = (-1, 0)
    x,y = pos
    if y - 1 in yrange and data[x][y] == data[x][y - 1]:
        y -= 1
        dir = (0, -1)
    elif x - 1 in xrange and data[x][y] == data[x - 1][y]:
        x -= 1
    elif y + 1 in yrange and data[x][y] == data[x][y + 1]:
        y += 1
        dir = (0, 1)
    else:
        x += 1
        dir = (1, 0)
    return dir, (x,y)

def going_right(pos):
    dir = (0,1)
    x,y = pos
    if x - 1 in xrange and data[x][y] == data[x - 1][y]:
        x -= 1
        dir = (-1, 0)
    elif y + 1 in yrange and data[x][y] == data[x][y + 1]:
        y += 1
    elif x + 1 in xrange and data[x][y] == data[x + 1][y]:
        #print("Found down")
        x += 1
        dir = (1, 0)
    else:
        y -= 1
        dir = (0, -1)
    return dir, (x,y)

def going_down(pos):
    dir = (1, 0)
    x,y = pos
    if y + 1 in yrange and data[x][y] == data[x][y + 1]:
        y += 1
        dir = (0, 1)
    elif x + 1 in xrange and data[x][y] == data[x + 1][y]:
        x += 1
    elif y - 1 in yrange and data[x][y] == data[x][y - 1]:
        y -= 1
        dir = (0, -1)
    else:
        x -= 1
        dir = (-1, 0)
    return dir, (x,y)

def going_left(pos):
    dir = (0, -1)
    x,y = pos
    if x + 1 in xrange and data[x][y] == data[x + 1][y]:
        x += 1
        dir = (1, 0)
    elif y - 1 in yrange and data[x][y] == data[x][y - 1]:
        y -= 1
    elif x - 1 in xrange and data[x][y] == data[x - 1][y]:
        x -= 1
        dir = (-1, 0)
    else:
        y += 1
        dir = (0, 1)
    return dir, (x,y)

def find_sides(pos):
    count = 0
    x, y = pos
    fdor = dirs[0]
    dir = dirs[0]
    npos = (x,y)
    while True:
        #if pos == (0,6):
            #time.sleep(1)
            #print(f"{npos} {dir}")
        if dir == (-1, 0):
            #print("Going up")
            ndir, npos = going_up(npos)
            if ndir != dir:
                if ndir == (1, 0):
                    count += 1
                count += 1
                dir = ndir
        elif dir == (0, 1):
            #print("Going right")
            ndir, npos = going_right(npos)
            if ndir != dir:
                if ndir == (0, -1):
                    count += 1
                count += 1
                dir = ndir
        elif dir == (1, 0):
            #print("Going down")
            ndir, npos = going_down(npos)
            if ndir != dir:
                if ndir == (-1, 0):
                    count += 1
                count += 1
                dir = ndir
        elif dir == (0, -1):
            #print("Going left")
            ndir, npos = going_left(npos)
            if ndir != dir:
                if ndir == (0, 1):
                    count += 1
                count += 1
                dir = ndir
        if npos == pos and dir == fdor:
            count += 1
            break
    print(f"{pos}: {count}")
    return count

visited = set()
plots = []
partone = 0
parttwo = 0
for x in range(len(data)):
    for y in range(len(data[x])):
        pos = x, y
        if pos in visited:
            continue
        plot = find_plot(pos, set())
        visited.update(plot)
        plots.append(plot)
        partone += find_edges(plot) * len(plot)
        if len(plot) == 1:
            parttwo += 4
            continue
        parttwo += find_sides(pos) * len(plot)

temp = find_sides((0,0))

end_time = time.time() - start_time

print(partone)
print(parttwo)
print(f"Time taken: {end_time*1000:3f} ms")