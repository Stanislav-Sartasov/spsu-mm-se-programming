package hashtable

import hashtable.HashTable.Entry
import hashtable.TestUtils.asArguments
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals
import hashtable.chained.MutableChainedHashTable.Companion.of as mutableHashTableOf

internal class HashTablesTest {

    @ParameterizedTest
    @MethodSource("hashTablesWithIsEmptyReturns")
    fun `is empty`(table: HashTable<String, Int>, isEmptyReturn: Boolean) {
        assertEquals(expected = isEmptyReturn, actual = table.isEmpty())
    }

    @ParameterizedTest
    @MethodSource("hashTablesWithIsNotEmptyReturns")
    fun `is not empty`(table: HashTable<String, Int>, isNotEmptyReturn: Boolean) {
        assertEquals(expected = isNotEmptyReturn, actual = table.isNotEmpty())
    }

    @ParameterizedTest
    @MethodSource("hashTablesWithContainsKeyReturns")
    fun `contains key`(table: HashTable<String, Int>, containsKeyReturns: List<Boolean>) {
        assertEquals(
            expected = containsKeyReturns,
            actual = TestUtils.containsKeyArguments.map(table::contains)
        )
    }

    @ParameterizedTest
    @MethodSource("hashTablesWithContainsEntryReturns")
    fun `contains entry`(table: HashTable<String, Int>, containsEntryReturns: List<Boolean>) {
        assertEquals(
            expected = containsEntryReturns,
            actual = TestUtils.containsEntryArguments.map(table::contains)
        )
    }

    @ParameterizedTest
    @MethodSource("hashTablesWithCountedEntries")
    fun iterator(table: HashTable<String, Int>, countedEntries: Map<Entry<String, Int>, Int>) {
        assertEquals(
            expected = countedEntries,
            actual = buildList { for (e in table) add(e) }.groupingBy { it }.eachCount()
        )
    }

    @ParameterizedTest
    @MethodSource("setArguments")
    fun `set 1 element to hash table`(
        table: MutableHashTable<String, Int>,
        entryToPut: Entry<String, Int>,
        expectedTableAfterSet: MutableHashTable<String, Int>,
    ) {
        table[entryToPut.key] = entryToPut.value
        assertEquals(expected = expectedTableAfterSet, actual = table)
    }


    @ParameterizedTest
    @MethodSource("hashTablesWithMaps")
    fun `convert hash table to map`(table: HashTable<String, Int>, map: Map<String, Int>) {
        assertEquals(expected = map, actual = table.toMap())
    }

    @ParameterizedTest
    @MethodSource("hashTablesWithMutableMaps")
    fun `convert hash table to mutable map`(table: HashTable<String, Int>, mutableMap: MutableMap<String, Int>) {
        assertEquals(expected = mutableMap, actual = table.toMutableMap())
    }


    @Test
    fun `covert entry to pair`() {
        assertEquals(expected = 17 to "xyz", actual = Entry(17, "xyz").toPair())
    }

    @Test
    fun `convert pair to entry`() {
        assertEquals(expected = Entry(17, "xyz"), actual = (17 to "xyz").toHashTableEntry())
    }


    private companion object {

        @JvmStatic
        private fun hashTablesWithIsEmptyReturns() = (tables zip TestUtils.isEmptyReturns).asArguments()

        @JvmStatic
        private fun hashTablesWithIsNotEmptyReturns() = (tables zip TestUtils.isNotEmptyReturns).asArguments()

        @JvmStatic
        private fun hashTablesWithContainsKeyReturns() = (tables zip TestUtils.containsKeyReturns).asArguments()

        @JvmStatic
        private fun hashTablesWithContainsEntryReturns() = (tables zip TestUtils.containsEntryReturns).asArguments()

        @JvmStatic
        private fun hashTablesWithCountedEntries() = (tables zip TestUtils.countedEntries).asArguments()

        @JvmStatic
        private fun setArguments() = listOf(
            Triple(mutableHashTableOf(), Entry("A", 1), mutableHashTableOf("A" to 1)),
            Triple(mutableHashTableOf("A" to 1), Entry("B", 2), mutableHashTableOf("A" to 1, "B" to 2)),
            Triple(mutableHashTableOf("A" to 1), Entry("A", 1), mutableHashTableOf("A" to 1)),
            Triple(mutableHashTableOf("A" to 1), Entry("A", 2), mutableHashTableOf("A" to 2)),
        ).asArguments()

        @JvmStatic
        private fun hashTablesWithMaps() = (tables zip TestUtils.maps).asArguments()

        @JvmStatic
        private fun hashTablesWithMutableMaps() = (tables zip TestUtils.mutableMaps).asArguments()


        private val tables = TestUtils.hashTables
    }
}
