package collections

import org.junit.jupiter.api.assertAll
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import java.util.concurrent.ConcurrentSkipListSet
import kotlin.concurrent.thread
import kotlin.random.Random
import kotlin.streams.asStream


class SetTest {
    @ParameterizedTest
    @MethodSource("sets")
    fun add(set: ConcurrentSet<Int>) {
        repeat(10) {
            var res = set.add(it)
            assert(res)
            assert(set.size == it + 1)

            res = set.add(it)
            assert(!res)
            assert(set.size == it + 1)
        }
    }

    @ParameterizedTest
    @MethodSource("sets")
    fun remove(set: ConcurrentSet<Int>) {
        repeat(10) { set.add(it) }

        repeat(10) {
            var res = set.remove(it)
            assert(res)
            assert(set.size == 10 - (it + 1))

            res = set.remove(it)
            assert(!res)
            assert(set.size == 10 - (it + 1))
        }
    }

    @ParameterizedTest
    @MethodSource("sets")
    fun contains(set: ConcurrentSet<Int>) {
        repeat(10) { set.add(it) }

        repeat(10) {
            var res = set.contains(it)
            assert(res)
            assert(set.size == 10 - it)

            set.remove(it)
            res = set.contains(it)
            assert(!res)
            assert(set.size == 10 - (it + 1))
        }
    }

    @ParameterizedTest
    @MethodSource("sets")
    fun concurrent(set: ConcurrentSet<Int>) {
        val javaSet = ConcurrentSkipListSet<Int>()
        javaSet.addAll(0 until 1000)

        val threads = List(12) {
            thread(start = false) {
                repeat(1000) {
                    val v = Random.nextInt(from = 0, until = 1000)
                    if (Random.nextBoolean()) {
                        if (javaSet.remove(v)) {
                            set.add(v)
                        }
                    } else {
                        if (set.remove(v)) {
                            javaSet.add(v)
                        }
                    }
                }
            }
        }
        threads.forEach(Thread::start)
        threads.forEach(Thread::join)

        assertAll(
            generateSequence(0) {
                if (it < 999) it + 1 else null
            }.map {
                { assert(javaSet.contains(it) xor set.contains(it)) { "$it not found" } }
            }.asStream()
        )
    }

    companion object {
        @JvmStatic
        fun sets() = listOf<ConcurrentSet<Int>>(
            LockFreeListSet(),
            StripedCuckooHashSet()
        )
    }
}
