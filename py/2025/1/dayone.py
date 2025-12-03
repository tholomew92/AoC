import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

data = inputfile.readlines()

p1 = p2 = 0
curpos = prepos = start = 50
for line in data:
    steps = int(line.replace("L","-").strip("R\n"))
    prepos = curpos
    curpos += steps
    if curpos % 100 == 0:
        p1 += 1
    p2 += abs((curpos - prepos) // 100)

end_time = time.time() - start_time

print(f"Part one: {p1}")
print(f"Part two: {p2}")
print(f"Time taken: {end_time:3f} s")