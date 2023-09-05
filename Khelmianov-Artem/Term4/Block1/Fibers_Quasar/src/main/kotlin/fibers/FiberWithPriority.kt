package fibers

import co.paralleluniverse.fibers.Fiber


class FiberWithPriority(
    val fiber: Fiber<Unit>,
    prio: Int
) {
    var prio: Int = prio
        set(value) {
            field = value.coerceIn(0..100)
        }
}
