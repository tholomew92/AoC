import os

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

lines = inputfile.readlines()
partone = 0
safe = 0

def validate_safety(split):
    inc = 0
    dec = 0
    for x in range(0,len(split)-1):
        this = int(split[x])
        next = int(split[x+1])
        if x < len(split) - 2:
            nextnext = int(split[x+2])
            print(f"This: {this}, Next: {next}, Nextnext: {nextnext}")
        diff = abs(next - this)
        if diff < 1  or diff > 3:
            nextdiff = abs(nextnext - this)
            print(f"{this} - {nextnext} equals")
            print(nextdiff)
            if x == len(split) - 2:
                return x + 1
            elif nextdiff >= 1 and nextdiff <= 3:
                return x + 1
            return x
        if this < next:
            inc = inc + 1
        elif this > next:
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
debug = True
for line in lines:
    split = line.split(' ')
    res = validate_safety(split)
    if res != -1:
        resone = res
        del split[res]
        res = validate_safety(split)
        if res != -1 and debug:
            print(line.strip())
            print(resone)
            print(res)
            print()
    else:
        partone = partone + 1
    if res == -1:
        safe = safe + 1

print(f"Part One: {partone}")
print(f"Part Two: {safe}")

