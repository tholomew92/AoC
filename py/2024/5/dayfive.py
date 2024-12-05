import os
here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(testfilepath)]

ordering = dict()

parse = True
partone = 0
parttwo = 0

for d in data:
    if d.strip() == "":
        parse = False
        continue
    if parse:
        key, val = d.split('|')
        if key not in ordering:
            ordering[key] = []
        ordering[key].append(val)
    if not parse:
        pages = d.split(',')
        correct = True
        incind = -1
        for x in reversed(range(len(pages))):
            page = pages[x]
            if page in ordering:
                overlap = any(check in pages[:x] for check in ordering[page])
                if overlap:
                    correct = False
        
        ind = int(len(pages) / 2)
        if correct:
            partone = partone + int(pages[ind])
        if not correct:
            incorrect = True
            copy = pages.copy()
            while incorrect: 
                incorrect = False
                for x in reversed(range(len(pages))):
                    curval = pages[x]
                    for y in reversed(range(x)):
                        nextval = pages[y]
                        if curval in ordering:
                            if nextval in ordering[curval]:
                                #print(copy)
                                del(copy[x])
                                copy.insert(y,curval)
                                break
                    if incorrect:
                        print(copy)
                        break
            print(copy)
            print(copy[ind])
            parttwo = parttwo + int(copy[ind])


print(partone)
print(parttwo)