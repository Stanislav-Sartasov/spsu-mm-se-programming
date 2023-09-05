#! /usr/bin/env python

import random
import subprocess as sp

with open('./input.txt', 'r') as fi:
    with open('./answer.txt', 'w') as fa:
        print(" ".join(map(str, sorted(map(int, fi.read().split())))), end='', file=fa)
print("OK" if not sp.run(["diff", "--brief", "output.txt", "answer.txt"], stdout=sp.DEVNULL).returncode else "FAIL")

