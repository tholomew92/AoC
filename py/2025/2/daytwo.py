import os
import math
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

file = [line.rstrip() for line in open(inputfilepath)]
data = file[0]

idranges = data.split(',')

def find_factors(num):
    factors = []
    for i in range(1, int(math.sqrt(num)) + 1):
        if i == num:
            break
        if num % i == 0:
            factors.append(i)
            if i != num // i:
                factors.append(num // i)
    if num in factors:
        factors.remove(num)
    return sorted(factors)

p1 = 0
p2 = 0
loops = 0
for idrange in idranges:
    split = idrange.split('-')
    start = int(split[0])
    end = int(split[1]) + 1
    for i in range(start, end):
        parttwo = False
        vstr = str(i)
        slen = len(vstr)
        if slen % 2 == 0:
            v1 = vstr[0:slen//2]
            v2 = vstr[slen//2:]
            if v1 == v2:
                parttwo = True
                p1 += i
        if not parttwo:
            factors = find_factors(slen)
            for factor in factors:
                sets = [vstr[j:j+factor] for j in range(0, slen, factor)]
                if len(set(sets)) == 1:
                    parttwo = True
                    break
        if parttwo:
            p2 += i

end_time = time.time() - start_time

print(f"Part one: {p1}")
print(f"Part two: {p2}")
print(f"Time taken: {end_time:3f} s")