import os
import time
import re

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]

positions = []
dirs = ((-1, 0),(0, 1),(1, 0),(0, -1))
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

def turn(pos, dirind):
    x, y = pos
    if dirind == 0:
        dirind = 4
    dirind -= 1
    ndirind = -1
    for i in range(len(dirs)):
        dx, dy = dirs[dirind]
        if x + dx in xrange and y + dy in yrange:
            if data[x][y] == data[x + dx][y + dy]:
                pos = (x+dx,y+dy)
                ndirind = dirind
                break
        dirind += 1
        if dirind == 4:
            dirind = 0

    return pos, ndirind

def find_sides(pos, plot):

    count = 0
    tx, ty = pos
    target = data[tx][ty]
    rowsstart = dict()
    rowsend = dict()
    colsstart = dict()
    colssend = dict()

    for x in xrange:
        rowsstart[x] = []
        rowsend[x] = []
        row = ''.join(data[x])
        pattern = re.escape(target) + r'+'
        matches = re.finditer(pattern, row)
        sections = [(match.start(), match.end() - 1) for match in matches]
        for section in sections:
            sy, ey = section
            if (x,sy) in plot:
                rowsstart[x].append(sy)
            if (x, sy) in plot:
                rowsend[x].append(ey)
            
    for x in xrange:
        if x == 0:
            count += len(rowsstart[0])
            count += len(rowsend[0])
        else:
            for start in rowsstart[x]:
                if start not in rowsstart[x - 1]:
                    count += 1
            for end in rowsend[x]:
                if end not in rowsend[x - 1]:
                    count += 1
        
    cv = count

    for y in yrange:
        colsstart[y] = []
        colssend[y] = []
        columndata = [col[y] for col in data]
        column = ''.join(columndata)
        pattern = re.escape(target) + r'+'
        matches = re.finditer(pattern, column)
        sections = [(match.start(), match.end() - 1) for match in matches]
        for section in sections:
            sx, ex = section
            if(sx,y) in plot:
                colsstart[y].append(sx)
            if(ex, y) in plot:
                colssend[y].append(ex)

    for y in yrange:
        if y == 0:
            count += len(colsstart[0])
            count += len(colssend[0])
        else:
            for start in colsstart[y]:
                if start not in colsstart[y - 1]:
                    count += 1
            for end in colssend[y]:
                if end not in colssend[y - 1]:
                    count += 1
   
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
        parttwo += find_sides(pos, plot) * len(plot)

end_time = time.time() - start_time

print(partone)
print(parttwo)
print(f"Time taken: {end_time:3f} s")