import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

file = [line.rstrip() for line in open(inputfilepath)]
data = file[0]

def file_block(ind, block):
    id = ind // 2
    bsize= int(block)
    sublist = []
    for i in range(bsize):
        sublist.append(str(id))
    return sublist

def free_space(block):
    bsize = int(block)
    sublist = []
    for i in range(bsize):
        sublist.append(".")
    return sublist

partone = []
for i in range(len(data)):
    if i % 2 == 0:
        partone += file_block(i, data[i])
    elif i % 2 == 1:
        partone += free_space(data[i])

pstring = []
backind = len(partone) - 1
for i in range(len(partone)):
    if i > backind:
        break
    place = partone[i]
    if place != '.':
        pstring.append(place)
        continue
    last = partone[backind]
    while last == '.':
        backind -= 1
        last = partone[backind]
    pstring.append(last)
    backind -= 1

p1 = 0
for i in range(len(pstring)):
    p1 += i * int(pstring[i])
print(''.join(pstring))
print(p1)