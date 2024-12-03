import os
import re
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
matchesone = re.findall(findpattern, data)
removed = re.sub(removepattern, "", data, flags=re.DOTALL)
matchestwo = re.findall(findpattern, removed)

for match in matchesone:
    val1, val2 = int(match[0]), int(match[1])
    partone = partone + val1 * val2

for match in matchestwo:
    val1, val2 = int(match[0]), int(match[1])
    parttwo = parttwo + val1 * val2

print(f"Part One: {partone}")
print(f"Part Two: {parttwo}")