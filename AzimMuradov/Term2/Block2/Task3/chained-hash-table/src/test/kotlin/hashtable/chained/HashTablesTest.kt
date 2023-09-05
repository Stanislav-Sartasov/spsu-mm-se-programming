package hashtable.chained

import hashtable.HashTable
import hashtable.chained.TestUtils.asArguments
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals
import kotlin.test.assertFalse

internal class HashTablesTest {

    @ParameterizedTest
    @MethodSource("chainedHashTables")
    fun `convert hash table to hash table`(table: HashTable<String, Int>) {
        assertEquals(expected = table, actual = table.toHashTable())
        assertFalse { table === table.toHashTable() }
    }

    @ParameterizedTest
    @MethodSource("chainedHashTables")
    fun `convert hash table to mutable hash table`(table: HashTable<String, Int>) {
        assertEquals(expected = table, actual = table.toMutableHashTable())
        assertFalse { table === table.toMutableHashTable() }
    }

    @ParameterizedTest
    @MethodSource("mapsWithChainedHashTables")
    fun `convert map to hash table`(map: Map<String, Int>, table: HashTable<String, Int>) {
        assertEquals(expected = table, actual = map.toHashTable())
    }

    @ParameterizedTest
    @MethodSource("mapsWithChainedHashTables")
    fun `convert map to mutable hash table`(map: Map<String, Int>, table: HashTable<String, Int>) {
        assertEquals(expected = table, actual = map.toMutableHashTable())
    }


    private companion object {

        @JvmStatic
        private fun chainedHashTables() = TestUtils.chainedHashTables.asArguments()

        @JvmStatic
        private fun mapsWithChainedHashTables() = (TestUtils.maps zip TestUtils.chainedHashTables).asArguments()
    }
}
