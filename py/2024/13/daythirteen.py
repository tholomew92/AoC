import os
import time
import re
from sympy import symbols, Eq, solve

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
inputfilepath = os.path.join(here, "input.txt")

data = [line.rstrip() for line in open(inputfilepath)]
pattern = r'\d+'

def do_math(buttona, buttonb, targets):
    x1, y1 = buttona
    x2, y2 = buttonb
    xtarget, ytarget = targets
    x_A, x_B = symbols('x_A x_B', integer=True)

    eq1 = Eq(x1 * x_A + x2 * x_B, xtarget)
    eq2 = Eq(y1 * x_A + y2 *x_B, ytarget)

    solutions = solve((eq1, eq2), (x_A, x_B), dict=True)

    valid_solutions = [sol for sol in solutions if sol[x_A] >= 0 and sol[x_B] >= 0]

    min_cost = float('inf')
    best_solution = None

    for sol in valid_solutions:
        cost = 3 * sol[x_A] + sol[x_B]
        if cost < min_cost:
            min_cost = cost
            best_solution = sol

    return min_cost, best_solution

partone = 0
parttwo = 0
for i in range(0,len(data),4):
    buttona = list(map(int, re.findall(pattern,data[i])))
    buttonb = list(map(int, re.findall(pattern,data[i+1])))
    targets = list(map(int, re.findall(pattern,data[i+2])))
    res, sol = do_math(buttona, buttonb, targets)
    if sol:
        partone += res
    targets = (10000000000000+targets[0], 10000000000000+targets[1])
    res, sol = do_math(buttona, buttonb, targets)
    if sol:
        parttwo += res

end_time = time.time() - start_time

print(f"Part One: {partone}")
print(f"Part One: {parttwo}")
print(f"Time taken: {end_time:3f} s")