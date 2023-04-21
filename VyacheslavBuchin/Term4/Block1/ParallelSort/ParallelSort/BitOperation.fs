module MPITask.BitOperation

let significantCnt num =
    let rec helper acc bit =
        if acc >= num then bit
        else helper (acc * 2) (bit + 1)
    helper 1 0

let invert i num = num ^^^ (1 <<< i)
let dropLast i num = (num >>> i) <<< i
let raiseLast i num = num + (1 <<< i) - 1
let get i num = ((1 <<< i) &&& num) >>> i
