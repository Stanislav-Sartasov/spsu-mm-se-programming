package hashtable.chained

import hashtable.HashTable
import hashtable.HashTable.Entry
import hashtable.chained.TestUtils.asArguments
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals

internal class ChainedHashTableTest {

    @ParameterizedTest
    @MethodSource("chainedHashTablesWithSizes")
    fun size(table: HashTable<String, Int>, size: Int) {
        assertEquals(expected = size, actual = table.size)
    }

    @ParameterizedTest
    @MethodSource("chainedHashTablesWithGetReturns")
    fun get(table: HashTable<String, Int>, getReturns: List<Int>) {
        assertEquals(expected = getReturns, actual = TestUtils.getArguments.map(table::get))
    }


    @ParameterizedTest
    @MethodSource("chainedHashTablesWithKeys")
    fun keys(table: HashTable<String, Int>, keys: Set<String>) {
        assertEquals(expected = keys, actual = table.keys.toSet())
    }

    @ParameterizedTest
    @MethodSource("chainedHashTablesWithValues")
    fun values(table: HashTable<String, Int>, values: List<Int>) {
        assertEquals(
            expected = values.groupingBy { it }.eachCount(),
            actual = table.values.groupingBy { it }.eachCount()
        )
    }

    @ParameterizedTest
    @MethodSource("chainedHashTablesWithEntries")
    fun entries(table: HashTable<String, Int>, entries: Set<Entry<String, Int>>) {
        assertEquals(expected = entries, actual = table.entries.toSet())
    }


    private companion object {

        @JvmStatic
        private fun chainedHashTablesWithSizes() = (tables zip TestUtils.sizes).asArguments()

        @JvmStatic
        private fun chainedHashTablesWithGetReturns() = (tables zip TestUtils.getReturns).asArguments()

        @JvmStatic
        private fun chainedHashTablesWithKeys() = (tables zip TestUtils.keys).asArguments()

        @JvmStatic
        private fun chainedHashTablesWithValues() = (tables zip TestUtils.values).asArguments()

        @JvmStatic
        private fun chainedHashTablesWithEntries() = (tables zip TestUtils.entries).asArguments()


        private val tables = TestUtils.chainedHashTables
    }
}
