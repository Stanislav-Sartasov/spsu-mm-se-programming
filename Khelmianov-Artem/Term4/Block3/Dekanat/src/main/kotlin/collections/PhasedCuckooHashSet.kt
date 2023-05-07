package collections

import java.util.concurrent.atomic.AtomicInteger

abstract class PhasedCuckooHashSet<T : Any>(
    @Volatile protected var capacity: Int
) : ConcurrentSet<T> {
    @Volatile
    protected var storage = Array(2) { Array(this.capacity) { mutableListOf<T>() } }

    protected val THRESHOLD = 16
    protected val LIST_SIZE = 32
    protected val RELOCATE_LIMIT = 5

    override val size: Int get() = _size.get()
    protected val _size = AtomicInteger(0)

    protected abstract fun acquire(element: T);
    protected abstract fun release(element: T);
    protected abstract fun resize();

    protected inline fun <R> withLock(element: T, block: () -> R): R {
        acquire(element)
        try {
            return block()
        } finally {
            release(element)
        }
    }

    override fun add(element: T): Boolean {
        var i: Int = 0
        var h: Int = 0
        var mustResize = false;

        withLock(element) {
            val h0 = element.hash(0)
            val h1 = element.hash(1)

            if (contains(element)) return false
            val s0 = storage[0, h0]
            val s1 = storage[0, h1]

            if (s0.size < THRESHOLD) {
                _size.incrementAndGet()
                s0.add(element)
                return true
            } else if (s1.size < THRESHOLD) {
                _size.incrementAndGet()
                s1.add(element)
                return true
            } else if (s0.size < LIST_SIZE) {
                i = 0
                h = h0
                _size.incrementAndGet()
                s0.add(element)
            } else if (s1.size < LIST_SIZE) {
                i = 1
                h = h1
                _size.incrementAndGet()
                s1.add(element)
            } else {
                mustResize = true
            }
        }
        if (mustResize) {
            resize()
            add(element)
        } else if (!relocate(i, h)) {
            resize()
        }
        return true
    }

    override fun remove(element: T): Boolean = withLock(element) {
        return if (storage[0, element.hash(0)].remove(element)) {
            _size.decrementAndGet()
            true
        } else if (storage[1, element.hash(1)].remove(element)) {
            _size.decrementAndGet()
            true
        } else false
    }

    override fun contains(element: T): Boolean = withLock(element) {
        if (element in storage[0, element.hash(0)]) true
        else element in storage[1, element.hash(1)]
    }

    @Suppress("NAME_SHADOWING")
    protected fun relocate(i: Int, hi: Int): Boolean {
        var i = i
        var hi = hi
        var hj: Int;
        var j = 1 - i;
        repeat(RELOCATE_LIMIT) {
            val iSet = storage[i, hi]
            val y = iSet/*[0]*/.getOrNull(0) ?: return@repeat
            hj = y.hash(j)
            withLock(y) {
                val jSet = storage[j, hj]
                if (iSet.remove(y)) {
                    if (jSet.size < THRESHOLD) {
                        jSet.add(y)
                        return true
                    } else if (jSet.size < LIST_SIZE) {
                        jSet.add(y)
                        i = 1 - i
                        hi = hj
                        j = 1 - j
                    } else {
                        iSet.add(y)
                        return false
                    }
                } else if (iSet.size < THRESHOLD) {
                    return true
                }
            }
        }
        return false
    }

    protected fun T.hash(array: Int, modulus: Int = capacity): Int {
        return if (array == 0)
            this.hashCode() % modulus
        else
            (this.hashCode() / modulus) % modulus
    }
}

internal operator fun <A> Array<Array<A>>.get(x: Int, y: Int): A = this[x][y]
internal fun <A> Array<Array<A>>.forEach(action: (A) -> Unit): Unit = forEach { it: Array<A> ->
    it.forEach { action(it) }
}
