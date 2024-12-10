import os
import time
from itertools import product

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]
debugvals = [641,109951,46913588,104773227,363819106,732348540]
partone = 0
parttwo = 0

test = data[0]

def do_compute(vals, target, parttwo=False):
    valsset = [vals[0]]
    for x in range(1, len(vals)):
        v = vals[x]
        tempset = []
        
        for val in valsset:
            prod = val * v
            if prod == target:
                print(f"{val} * {v}")
                return True
            if prod < target:
                tempset.append(prod)
            add = val + v
            if add == target:
                print(f"{val} + {v}")
                return True
            if add < target:
                tempset.append(add)
            if parttwo:
                pwr = 10 ** len(str(v))
                conc = val * pwr + v
                #conc = int(str(val)+str(v))
                if conc == target:
                    print(f"{val} conc {v}")
                    return True
                if conc < target:
                    tempset.append(conc)
            
        #if target in debugvals and parttwo:
            #print(tempset)
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

for line in data:
    split = [l.strip() for l in line.split(':')]
    target = int(split[0])
    vals = [int(d) for d in split[1].split(' ')]
    if target in debugvals:
        print(target)
    restwo = (do_compute(vals, target, True))
    if restwo:
        parttwo += target
    if target in debugvals:
        print()
        

parttwo_time = time.time() - partone_time - start_time

print(f"Part One: {partone} It took {partone_time*1000:.3f} ms")
print(f"Part Two: {parttwo} It took {parttwo_time*1000:.3f} ms")