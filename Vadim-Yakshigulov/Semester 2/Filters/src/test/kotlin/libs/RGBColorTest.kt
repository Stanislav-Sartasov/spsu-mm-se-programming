package libs

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import java.security.InvalidParameterException

internal class RGBColorTest {

    @Test
    fun `Illegal get value  trows exception`() {
        assertThrows(
            InvalidParameterException::class.java,
        ) { RGBColor(0, 0, 0)[1000] }

        assertThrows(
            InvalidParameterException::class.java,
        ) { RGBColor(0, 0, 0)[-1000] }

        assertThrows(
            InvalidParameterException::class.java,
        ) { RGBColor(0, 0, 0)[3] }
    }
}