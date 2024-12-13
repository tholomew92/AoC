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

def find_sides(pos, plot):

    countone = 0
    counttwo = 0
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
        countone += len(rowsstart[x])
        countone += len(rowsend[x])
        if x == 0:
            counttwo += len(rowsstart[0])
            counttwo += len(rowsend[0])
        else:
            for start in rowsstart[x]:
                if start not in rowsstart[x - 1]:
                    counttwo += 1
            for end in rowsend[x]:
                if end not in rowsend[x - 1]:
                    counttwo += 1

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
        countone += len(colsstart[y])
        countone += len(colssend[y])
        if y == 0:
            counttwo += len(colsstart[0])
            counttwo += len(colssend[0])
        else:
            for start in colsstart[y]:
                if start not in colsstart[y - 1]:
                    counttwo += 1
            for end in colssend[y]:
                if end not in colssend[y - 1]:
                    counttwo += 1
   
    return countone, counttwo

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
        totals = find_sides(pos, plot)
        partone += totals[0] * len(plot)
        parttwo += totals[1] * len(plot)

end_time = time.time() - start_time

print(partone)
print(parttwo)
print(f"Time taken: {end_time:3f} s")