import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

file = [line.rstrip() for line in open(testfilepath)]
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

p1copy = []
p2copy = []

for block in blocklist:
    p1copy.append(block)
    p2copy.append(block)


# Part One

backind = len(p1copy) - 1
p1list = []
ind = 0

for block in blocklist:
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
p2list = []
ind = 0


for i in range(len(blocklist)):
    
            
p2 = 0
p2str = ""

for val in p2list:
    if val != '.':
        p2str += str(val)
    else:
        p2str += val
print(p2str)
for i in range(len(p2str)):
    if p2str[i] != '.':
        p2 += i * int(p2str[i])

p2_time = time.time() - start_time

print(f"Part One: {p1} It took {p1_time*1000:.3f} ms")
print(f"Part Two: {p2} It took {p2_time*1000:.3f} ms")
