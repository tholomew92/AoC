import os

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

left = []
right = []
rocc = dict()

testlines = inputfile.readlines()
for line in testlines:
    split = line.split('   ')
    l, r = map(int, split)
    left.append(l)
    right.append(r)
    if r not in rocc:
        rocc[r] = 0
    rocc[r] = rocc[r] + 1

left.sort() 
right.sort()

diff = 0
sym = 0
ind = 0

for l in left:
    absval = abs(right[ind] - left[ind])
    diff = diff + absval
    if l in rocc:
        sym = sym + (l * rocc.get(l))
    ind = ind + 1

print(diff)
print(sym)