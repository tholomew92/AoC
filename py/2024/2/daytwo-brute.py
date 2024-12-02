import os
here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

lines = inputfile.readlines()
partone = 0
parttwo = 0

def validate_safety(split):
    inc = 0
    dec = 0
    for x in range(0,len(split)-1):
        this = int(split[x])
        next = int(split[x+1])
        diff = abs(next - this)
        saferen = range(1,4)
        if diff not in saferen:
            return False
        if this < next:
            inc = inc + 1
        else:
            dec = dec + 1
        if inc > 0 and dec > 0:
            return False    
    return True

for line in lines:
    split = line.split(' ')
    res = validate_safety(split)
    if res:
        partone = partone + 1
        parttwo = parttwo + 1
    else:
        for x in range(0,len(split)):
            copy = split[:x] + split[x+1:]
            res = validate_safety(copy)
            if res:
                parttwo = parttwo + 1
                break
print(f"Part One: {partone}")
print(f"Part Two: {parttwo}")
