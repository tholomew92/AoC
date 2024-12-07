import os
import time
from itertools import product

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(testfilepath)]

partone = 0
parttwo = 0

test = data[0]

def do_compute(vals, target):
    valsset = [vals[0]]
    for x in range(1, len(vals)):
        v = vals[x]
        tempset = set()
        for val in valsset:
            prod = val * v
            if prod == target:
                return True
            if prod < target:
                tempset.add(prod)
            
            add = val + v
            if add == target:
                return True
            if add < target:
                tempset.add(add)
        if not tempset:
            break
        valsset = tempset
    return False
        
            

for line in data:
    split = [l.strip() for l in line.split(':')]
    target = int(split[0])
    vals = [int(d) for d in split[1].split(' ')]
    resone = (do_compute(vals, target))
    if resone:
        partone += target

partone_time = time.time() - start_time


print(f"Part One: {partone} It took {partone_time*1000:.3f} ms")