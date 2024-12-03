import os
import re
here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

partone = 0
parttwo = 0

def get_matches(matches):
    res = 0
    for match in matches:
        res = int(match[0]) * int(match[1]) + res
    return res 

data = inputfile.read()
findpattern = r"mul\((-?\d+),\s*(-?\d+)\)"
removepattern = r"don't\(\).*?do\(\)"
matchesone = re.findall(findpattern, data)
removed = re.sub(removepattern, "", data, flags=re.DOTALL)
matchestwo = re.findall(findpattern, removed)

partone = get_matches(matchesone)
parttwo = get_matches(matchestwo)

print(f"Part One: {partone}")
print(f"Part Two: {parttwo}")