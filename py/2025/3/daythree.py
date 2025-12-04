import os

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

p1 = 0
p2 = 0
for line in testfile:
    v1 = v2 = v1pos = v2pos = -1
    data = list(map(int, line.rstrip()))
    v1 = max(data)
    v1pos = data.index(v1)
    if v1pos == len(data) - 1:
        v2 = v1
        v2pos = v1pos
        data.pop()
        v1 = max(data)
        v1pos = data.index(v1)
    else:
        v2 = max(data[v1pos+1:])
        v2pos = data.index(v2)
    
    p1 += int(str(v1)+str(v2))

    if v2pos - v1pos == 1:
        p2val = int(''.join(str(i) for i  in data[v1pos:v1pos+12]))
        print(p2val)
        p2 += p2val

print(f"Part one: {p1}")
print(f"Part two: {p2}")