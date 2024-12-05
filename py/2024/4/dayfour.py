import os

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

data = inputfile.readlines()

def downleft(x,y,data,steps):
    sublist = []
    for a in range(0,steps):
        sublist.append(data[x+a][y-a])
    return "".join(sublist)

def downright(x,y,data,steps):
    sublist = []
    for a in range(0,steps):
        sublist.append(data[x+a][y+a])
    return "".join(sublist)

def down(x,y,data,steps):
    sublist = []
    for a in range(0,steps):
        sublist.append(data[x+a][y])
    return "".join(sublist)

partone = 0
parttwo = 0
hitsone = ["XMAS", "SAMX"]
hitstwo = ["MAS", "SAM"]
for x in range(0, len(data)):
    for y in range(0, len(data[x].strip())):
        
        # Part One
        if y < len(data[x].strip()) - 3:
            substr = data[x][y:y+4]
            if substr in hitsone:
                partone = partone + 1
        if y > 2 and x < len(data) - 3:
            downleftone = downleft(x,y,data,4)
            if downleftone in hitsone:
                partone = partone + 1
        if x < len(data) - 3:
            downone = down(x,y,data,4)
            if downone in hitsone:
                partone = partone + 1
        if y < len(data[x].strip()) - 3 and x < len(data) - 3:
            downrightone = downright(x,y,data,4)
            if downrightone in hitsone:
                partone = partone + 1
        
        # Part Two
        if y < len(data[x].strip()) - 2 and x < len(data) - 2:
            downrighttwo = downright(x,y,data,3)
            if downrighttwo in hitstwo:
                downlefttwo = downleft(x,y+2,data,3)
                if downlefttwo in hitstwo:
                    parttwo = parttwo + 1

print(partone)
print(parttwo)