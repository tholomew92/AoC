import os
import re
import ast
here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

partone = 0
parttwo = 0

data = inputfile.read()
findpattern = r"mul\((-?\d+),\s*(-?\d+)\)"
removepattern = r"don't\(\).*?do\(\)"
matchesone = [(int(x), int(y)) for x, y in re.findall(findpattern, data)]
removed = re.sub(removepattern, "", data, flags=re.DOTALL)
matchestwo = [(int(x), int(y)) for x, y in re.findall(findpattern, removed)]

partone = sum(a * b for a,b in matchesone)
parttwo = sum(a * b for a,b in matchestwo)


print(f"Part One: {partone}")
print(f"Part Two: {parttwo}")