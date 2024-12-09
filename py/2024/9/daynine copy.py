import os
import time
from copy import deepcopy

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

file = [line.rstrip() for line in open(inputfilepath)]
data = file[0]

blocklist = []
index = 0
for i in range(len(data)):
    tdict = dict()
    if i % 2 == 0:
        tdict[index] = int(data[i])
        blocklist.append(tdict)
        index += 1
    if i % 2 == 1:
        if data[i] != '0':
            tdict['.'] = int(data[i])
            blocklist.append(tdict)


p1copy = deepcopy(blocklist)
p2copy = deepcopy(blocklist)

# Part One

backind = len(p1copy) - 1
p1list = []
ind = 0

for block in p1copy:
    if ind > backind:
        break
    key = next(iter(block))
    if key != '.':
        for x in range(block[key]):
            p1list.append(key)
        ind += 1
    else:
        dotcount = block[key]
        while dotcount > 0:
            lblock = p1copy[-1]
            lkey = next(iter(lblock))
            if lkey == '.':
                del p1copy[-1]
                continue
            if lkey < ind:
                break
            p1list.append(lkey)
            lblock[lkey] -= 1
            if lblock[lkey] == 0:
                del p1copy[-1]
                backind -= 1
            dotcount -= 1
p1 = 0
for i in range(len(p1list)):
    p1 += i * int(p1list[i])

p1_time = time.time() - start_time

# Part Two

backind = len(p2copy) - 1
ind = 0
p2list = []

for block in p2copy:
    key = next(iter(block))
    for b in range(block[key]):
        p2list.append(key)

for i in reversed(range(len(p2copy))):
    block = p2copy[i]
    key = next(iter(block))
    if key == '.':
        continue
    length = block[key]
    endind = -1
    startind = -1
    for j in reversed(range(len(p2list))):
        if p2list[j] == key:
            endind = j + 1
            startind = endind - length
            break
    dotset = set()
    for k in range(len(p2list)):
        if k > startind:
            break
        if p2list[k] == '.':
            dotjoin = ''.join(str(s) for s in p2list[k:k+length])
            dotset = set(dotjoin)
            if len(dotset) == 1:
                p2list[k:k+length] = [key] * ((k+length) - k)
                p2list[startind:endind] = ['.'] * (endind - startind)
                break

p2 = 0

for i in range(len(p2list)):
    val = p2list[i]
    if val != '.':
        p2 += val * i

p2_time = time.time() - start_time

print(f"Part One: {p1} It took {p1_time*1000:.3f} ms")
print(f"Part Two: {p2} It took {p2_time*1000:.3f} ms")
