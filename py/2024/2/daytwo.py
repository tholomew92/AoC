import os

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

lines = inputfile.readlines()
partone = 0
parttwo = 0

def validate_parttwoty(split):
    inc = 0
    dec = 0
    last = len(split) - 2
    for x in range(0,len(split)-1):
        this = int(split[x])
        next = int(split[x+1])
        if x < last:
            nextnext = int(split[x+2])
            nextdiff = abs(nextnext - this)
        diff = abs(next - this)
        saferen = range(1,4)
        if diff not in saferen:
            if x == last:
                return x + 1
            elif nextdiff in saferen:
                return x + 1
            return x
        if this < next:
            inc = inc + 1
        else:
            dec = dec + 1
        if inc > 0 and dec > 0:
            if x == 1 and len(split) > 3:
                if this < next and next < nextnext:
                    return 0
                elif this > next and next > nextnext:
                    return 0
            elif x == len(split) - 2:
                if this < int(split[x-1]) and int(split[x-1]) < int(split[x+-2]):
                    return x + 1
                elif this > int(split[x-1]) and int(split[x-1]) > int(split[x-2]):
                    return x + 1
            elif dec == 1:
                if this < nextnext:
                    return x + 1
            elif inc == 1:
                if this > nextnext:
                    return x + 1
            return x
    return -1
for line in lines:
    split = line.split(' ')
    res = validate_parttwoty(split)
    if res != -1:
        resone = res
        del split[res]
        res = validate_parttwoty(split)
    else:
        partone = partone + 1
    if res == -1:
        parttwo = parttwo + 1

print(f"Part One: {partone}")
print(f"Part Two: {parttwo}")

