import os
here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(testfilepath)]

ordering = dict()

parse = True
partone = 0
parttwo = 0

def validaterow(pages):
    for x in reversed(range(len(pages))):
            page = pages[x]
            print(page)
            if page in ordering:
                for y in reversed(range(x - 1)):
                    npage = pages[y]
                    print(y)
                    print(f"Checking if {npage} is in {page}:{ordering[page]}")
                    if npage in ordering[page]:
                            print("hit")
                            return x,y
    return -1

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
        index = validaterow(pages)
        ind = int(len(pages) / 2)
        print(f"In {pages} at index: {index}")
        print()
        print()
        continue
        if index == -1:
            partone = partone + int(pages[ind])
        else:
            incorrect = True
            copy = pages.copy()
            while True:
                done = False
                val = copy[index]
                del copy[index]
                for x in range(len(copy)):
                    if copy[x] in ordering[val]:
                        copy.insert(x,val)
                        break
                index = validaterow(copy)    
                if index == -1:
                    parttwo = parttwo + int(copy[ind])
                    break
                break
for k in ordering:
     print(k)

print(partone)
print(parttwo)