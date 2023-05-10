package chat

import kotlin.test.Test
import kotlin.test.assertNotNull
import kotlin.test.assertNull


class UtilsTest {

    @Test
    fun `try to run code and get not null value`() {
        assertNotNull(tryOrNull { 17 })
    }

    @Test
    fun `try to run code and get null value due to exception`() {
        assertNull(tryOrNull<Int> { error("") })
    }
}
