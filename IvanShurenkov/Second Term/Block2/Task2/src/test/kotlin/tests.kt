import hashTable.HashTable
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class HelloJunit5Test {
    @Test
    fun `Test add, get function`() {
        val hashTable = HashTable<Int>(11)
        val values = arrayOf(1, 2, 3, 4, 5, 6, 7, 8, 9, 0)
        val keys = arrayOf(11, 12, 13, 14, 15, 16, 17, 18, 19, 22)

        for (i in values.indices) {
            hashTable.add(keys[i], values[i])
            assertEquals(values[i], hashTable.get(keys[i]))
        }
        for (i in values.indices) {
            assertEquals(values[i], hashTable.get(keys[i]))
        }

        assertEquals(null, hashTable.get(0))
    }

    @Test
    fun `Test double add by one key`() {
        val hashTable = HashTable<Int>(11)
        hashTable.add(1, 1)
        assertEquals(1, hashTable.get(1))
        assertEquals(1, hashTable.size)
        hashTable.add(1, 2)
        assertEquals(2, hashTable.get(1))
        assertEquals(1, hashTable.size)
    }

    @Test
    fun `Test remove function`() {
        val hashTable = HashTable<Int>(11)
        val values = arrayOf(1, 2, 3, 4, 5, 6, 7, 8, 9, 0)
        val keys = arrayOf(11, 12, 13, 14, 15, 16, 17, 18, 19, 22)
        for (i in values.indices) {
            hashTable.add(keys[i], values[i])
        }

        for (i in values.indices) {
            hashTable.remove(keys[i])
            assertEquals(null, hashTable.get(keys[i]))
        }
        hashTable.remove(0)
    }

    @Test
    fun `Test size function`() {
        val hashTable = HashTable<Int>(11)
        val values = arrayOf(1, 2, 3, 4, 5, 6, 7, 8, 9, 0)
        val keys = arrayOf(11, 12, 13, 14, 15, 16, 17, 18, 19, 22)
        var cnt = 0

        assertEquals(cnt, hashTable.size)
        for (i in values.indices) {
            hashTable.add(keys[i], values[i])
            cnt++
            assertEquals(cnt, hashTable.size)
        }
        for (i in values.indices) {
            hashTable.remove(keys[i])
            cnt--
            assertEquals(cnt, hashTable.size)
        }
    }

    @Test
    fun `Test rebalance`() {
        val hashTable = HashTable<Int>(2)
        val values = arrayOf(1, 2, 3, 4, 5, 6, 7, 8, 9, 0)
        val keys = arrayOf(11, 12, 13, 14, 15, 16, 17, 18, 19, 22)
        var cnt = 0

        assertEquals(cnt, hashTable.size)
        for (i in values.indices) {
            hashTable.add(keys[i], values[i])
            cnt++
            assertEquals(cnt, hashTable.size)
            assertEquals(values[i], hashTable.get(keys[i]))
        }

        assertEquals(null, hashTable.get(0))

        for (i in values.indices) {
            hashTable.remove(keys[i])
            cnt--
            assertEquals(cnt, hashTable.size)
            assertEquals(null, hashTable.get(keys[i]))
        }
    }

    @Test
    fun `Test all function with Type = String`() {
        val hashTable = HashTable<String>(2)
        val values = arrayOf("1", "2", "3", "4", "5", "6", "7", "8", "9", "0")
        val keys = arrayOf(11, 12, 13, 14, 15, 16, 17, 18, 19, 22)
        var cnt = 0

        assertEquals(cnt, hashTable.size)
        for (i in values.indices) {
            hashTable.add(keys[i], values[i])
            cnt++
            assertEquals(cnt, hashTable.size)
            assertEquals(values[i], hashTable.get(keys[i]))
        }

        assertEquals(null, hashTable.get(0))

        for (i in values.indices) {
            hashTable.remove(keys[i])
            cnt--
            assertEquals(cnt, hashTable.size)
            assertEquals(null, hashTable.get(keys[i]))
        }
    }
}