import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]

corridor =  []

split = [int(d) for d in data[0].split(' ')]

def dayeleven(values, loops):
    dictval = dict()
    for v in values:
        dictval[v] = 1
    for i in range(loops):
        tempdict = dict()
        for v in dictval:
            if v == 0:
                if 1 not in tempdict:
                    tempdict[1] = 0
                tempdict[1] += dictval[v]
            elif len(str(v)) % 2 == 0:
                v1 = v // (10** (len(str(v))//2))
                v2 = v % (10** (len(str(v))//2))
                if v1 not in tempdict:
                    tempdict[v1] = 0
                tempdict[v1] += dictval[v]
                if v2 not in tempdict:
                    tempdict[v2] = 0
                tempdict[v2] += dictval[v]
            else:
                if v * 2024 not in tempdict:
                    tempdict[v * 2024] = 0
                tempdict[v * 2024] = dictval[v]
        dictval = tempdict
    return dictval

p1dict = dayeleven(split, 25)
p1 = 0
for key in p1dict:
    p1 += p1dict[key]

p1_time = time.time() - start_time

p2dict = dayeleven(split, 75)

p2 = 0
for key in p2dict:
    p2 += p2dict[key]

p2_time = time.time() - p1_time - start_time

print(f"Part One: {p1} It took {p1_time*1000:.3f} ms")
print(f"Part Two: {p2} It took {p2_time*1000:.3f} ms")