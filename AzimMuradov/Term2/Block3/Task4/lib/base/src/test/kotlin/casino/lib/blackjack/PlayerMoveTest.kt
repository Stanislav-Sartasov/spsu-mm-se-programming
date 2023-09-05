package casino.lib.blackjack

import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

internal class PlayerMoveTest {

    @Test
    fun `invert HIT to STAND`() {
        assertEquals(expected = PlayerMove.STAND, actual = PlayerMove.HIT.invert())
    }

    @Test
    fun `invert STAND to HIT`() {
        assertEquals(expected = PlayerMove.HIT, actual = PlayerMove.STAND.invert())
    }
}
