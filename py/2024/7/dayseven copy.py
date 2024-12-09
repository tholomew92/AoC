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

def valid_concat(val, valset, target):
    tryval = do_compute(val, target, False)
    if tryval:
        return True

    return False

def do_compute(vals, target, parttwo):
    valsset = [vals[0]]
    prevvals = []
    for x in range(1, len(vals)):
        v = vals[x]
        tempset = []
        if parttwo:
            pwr = len(str(v))
            conc = vals[x-1] * (10 ** pwr) + v
            if conc == target:
                print(target)
                return True
        for val in valsset:
            prod = val * v
            if prod == target:
                return True
            if prod < target:
                tempset.append(prod)
            
            add = val + v
            if add == target:
                return True
            if add < target:
                tempset.append(add)
        if not tempset:
            break
        valsset = tempset
        tempset.append(v)
        if x == 1:
            tempset.append(vals[0])
        for pval in prevvals:
            for tval in tempset:
                tset = pval
                tset.append(tval)
                test = do_compute(tset, target, False)
                if test:
                    return True
        if parttwo:
            prevvals.append(tempset)
    return False
        
            

for line in data:
    split = [l.strip() for l in line.split(':')]
    target = int(split[0])
    vals = [int(d) for d in split[1].split(' ')]
    resone = (do_compute(vals, target, False))
    if resone:
        partone += target

partone_time = time.time() - start_time

for line in data:
    split = [l.strip() for l in line.split(':')]
    target = int(split[0])
    vals = [int(d) for d in split[1].split(' ')]
    restwo = (do_compute(vals, target, True))
    if restwo:
        parttwo += target

parttwo_time = time.time() - partone_time
com_time = time.time() - start_time

print(f"Part One: {partone} It took {partone_time*1000:.3f} ms")
print(f"Part Two: {parttwo} It took {parttwo_time:.3f} ms")
print(com_time)