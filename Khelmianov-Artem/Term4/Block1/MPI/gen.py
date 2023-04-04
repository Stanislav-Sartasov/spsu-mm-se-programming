#! /usr/bin/env python

import random
import sys

with open('./input.txt', 'w') as f:
    n = int(sys.argv[1])
    print(" ".join(map(str, (random.randint(0, n) for x in range(n)))), end='', file=f)


