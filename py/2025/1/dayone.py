import os
import time

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

data = inputfile.readlines()
p1 = p2 = 0
curval = preval = start = 50
loops = 0
prevsteps = steps = 0
for line in data:
    prevsteps = steps
    steps = int(line.replace("L","-").strip("R\n"))
    preval = curval
    curval += steps
    if curval % 100 == 0:
        p1 += 1
    p2 += abs(curval // 100 - preval // 100) + (preval % 100 == 0) * ((prevsteps < 0 and steps > 0) - (prevsteps > 0 and steps < 0))

print(f"Part one: {p1}")
print(f"Part two: {p2}")