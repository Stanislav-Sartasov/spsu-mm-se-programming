package hashtable.chained

import hashtable.HashTable.Entry
import hashtable.MutableHashTable
import hashtable.chained.TestUtils.asArguments
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals
import hashtable.chained.ChainedHashTable.Companion.of as hashTableOf
import hashtable.chained.MutableChainedHashTable.Companion.of as mutableHashTableOf

internal class MutableChainedHashTableTest {

    @ParameterizedTest
    @MethodSource("mutableChainedHashTablesWithSizes")
    fun size(table: MutableHashTable<String, Int>, size: Int) {
        assertEquals(expected = size, actual = table.size)
    }

    @ParameterizedTest
    @MethodSource("mutableChainedHashTablesWithGetReturns")
    fun get(table: MutableHashTable<String, Int>, getReturns: List<Int>) {
        assertEquals(expected = getReturns, actual = TestUtils.getArguments.map(table::get))
    }


    @ParameterizedTest
    @MethodSource("mutableChainedHashTablesWithKeys")
    fun keys(table: MutableHashTable<String, Int>, keys: Set<String>) {
        assertEquals(expected = keys, actual = table.keys.toSet())
    }

    @ParameterizedTest
    @MethodSource("mutableChainedHashTablesWithValues")
    fun values(table: MutableHashTable<String, Int>, values: List<Int>) {
        assertEquals(
            expected = values.groupingBy { it }.eachCount(),
            actual = table.values.groupingBy { it }.eachCount()
        )
    }

    @ParameterizedTest
    @MethodSource("mutableChainedHashTablesWithEntries")
    fun entries(table: MutableHashTable<String, Int>, entries: Set<Entry<String, Int>>) {
        assertEquals(expected = entries, actual = table.entries.toSet())
    }


    @ParameterizedTest
    @MethodSource("putArguments")
    fun `put 1 element to hash table`(
        table: MutableHashTable<String, Int>,
        entryToPut: Entry<String, Int>,
        expectedTableAfterPut: MutableHashTable<String, Int>,
        expectedReplacedValue: Int?,
    ) {
        val replacedValue = table.put(entryToPut.key, entryToPut.value)
        assertEquals(expected = expectedTableAfterPut, actual = table)
        assertEquals(expected = expectedReplacedValue, actual = replacedValue)
    }

    @ParameterizedTest
    @MethodSource("removeArguments")
    fun `remove 1 element from hash table`(
        table: MutableHashTable<String, Int>,
        keyToRemove: String,
        expectedTableAfterRemove: MutableHashTable<String, Int>,
        expectedRemovedValue: Int?,
    ) {
        val removedValue = table.remove(keyToRemove)
        assertEquals(expected = expectedTableAfterRemove, actual = table)
        assertEquals(expected = expectedRemovedValue, actual = removedValue)
    }


    @ParameterizedTest
    @MethodSource("mutableChainedHashTables")
    fun clear(table: MutableHashTable<String, Int>) {
        assertEquals(expected = emptyHashTable, actual = table.apply { clear() })
    }


    private companion object {

        @JvmStatic
        private fun mutableChainedHashTablesWithSizes() = (tables zip TestUtils.sizes).asArguments()

        @JvmStatic
        private fun mutableChainedHashTablesWithGetReturns() = (tables zip TestUtils.getReturns).asArguments()

        @JvmStatic
        private fun mutableChainedHashTablesWithKeys() = (tables zip TestUtils.keys).asArguments()

        @JvmStatic
        private fun mutableChainedHashTablesWithValues() = (tables zip TestUtils.values).asArguments()

        @JvmStatic
        private fun mutableChainedHashTablesWithEntries() = (tables zip TestUtils.entries).asArguments()

        @JvmStatic
        private fun putArguments() = listOf(
            listOf(mutableHashTableOf<String, Int>(), Entry("A", 1), mutableHashTableOf("A" to 1), null),
            listOf(mutableHashTableOf("A" to 1), Entry("B", 2), mutableHashTableOf("A" to 1, "B" to 2), null),
            listOf(mutableHashTableOf("A" to 1), Entry("A", 1), mutableHashTableOf("A" to 1), 1),
            listOf(mutableHashTableOf("A" to 1), Entry("A", 2), mutableHashTableOf("A" to 2), 1),
        ).asArguments()

        @JvmStatic
        private fun removeArguments() = listOf(
            listOf(mutableHashTableOf<String, Int>(), "A", mutableHashTableOf<String, Int>(), null),
            listOf(mutableHashTableOf("A" to 1), "A", mutableHashTableOf<String, Int>(), 1),
            listOf(mutableHashTableOf("A" to 1, "B" to 2), "A", mutableHashTableOf("B" to 2), 1),
        ).asArguments()

        @JvmStatic
        private fun mutableChainedHashTables() = tables.asArguments()


        private val tables = TestUtils.mutableChainedHashTables

        private val emptyHashTable = hashTableOf<String, Int>()
    }
}
