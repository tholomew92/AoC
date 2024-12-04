import os
here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

data = inputfile.readlines()

partone = 0
hits = ["XMAS", "SAMX"]
for x in range(0, len(data)):
    for y in range(0, len(data[x].strip())):
        hit = False
        if y < len(data[x].strip()) - 3:
            substr = data[x][y:y+4]
            if substr in hits:
                hit = True
                #print(f"x: {x}, y :{y} - Right")
                partone = partone + 1
        if y > 2 and x < len(data) - 3:
            dleft = []
            dleft.append(data[x][y])
            dleft.append(data[x+1][y-1])
            dleft.append(data[x+2][y-2])
            dleft.append(data[x+3][y-3])
            substr = "".join(dleft)
            if substr in hits:
                hit = True
                #print(f"x: {x}, y :{y} - Down Left")
                partone = partone + 1
        if x < len(data) - 3:
            down = []
            down.append(data[x][y])
            down.append(data[x+1][y])
            down.append(data[x+2][y])
            down.append(data[x+3][y])
            substr = "".join(down)
            if substr in hits:
                hit = True
                #print(f"x: {x}, y :{y} - Down")
                partone = partone + 1
        if y < len(data[x].strip()) - 3 and x < len(data) - 3:
            dright = []
            dright.append(data[x][y])
            dright.append(data[x+1][y+1])
            dright.append(data[x+2][y+2])
            dright.append(data[x+3][y+3])
            substr = "".join(dright)
            if substr in hits:
                hit = True
                #print(f"x: {x}, y :{y} - Down Right")
                partone = partone + 1
    

print(partone)           
