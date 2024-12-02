import os

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

lines = inputfile.readlines()
safe = 0

for line in lines:
    split = line.split(' ')
    inc = 0
    dec = 0
    bad = 0
    safety = True 
    for x in range(0,len(split)-1):
        this = int(split[x])
        next = int(split[x+1])
        diff = abs(next - this)
        
        if diff < 1 or diff > 3:
            if bad == 0:
                bad = bad + 1
            else:
                safety = False
                break
        if this < next:
            inc = inc + 1
        elif this > next:
            dec = dec + 1
        if inc > 0 and dec > 0:
            if bad == 0:
                bad = bad + 1
                inc = inc - 1
                dec = dec -1
            else:    
                break
    if safety and inc > 0 and dec > 0:
        safety = False
    if safety:
        safe = safe + 1

print(safe)