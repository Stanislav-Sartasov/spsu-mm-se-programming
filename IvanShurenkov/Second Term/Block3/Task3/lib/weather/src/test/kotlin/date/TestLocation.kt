package date

import lib.weather.date.Location
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class TestLocation {
    private fun equal(loc1: Location, ans: Pair<Double, Double>): Boolean {
        return loc1.lat == ans.first && loc1.lon == ans.second
    }

    @Test
    fun `Test init`() {
        val locations: List<Pair<Double, Double>> = listOf(
            Pair(3.3, 2.5), Pair(-210.0, -213.0), Pair(190.0, 180.1)
        )
        val correctLocation: List<Pair<Double, Double>> = listOf(
            Pair(3.3, 2.5), Pair(-90.0, -180.0), Pair(90.0, 180.0)
        )
        for (i in locations.indices) {
            assert(equal(Location(locations[i].first, locations[i].second), correctLocation[i]))
        }
    }

    @Test
    fun `Test toString`() {
        val locations: List<Pair<Double, Double>> = listOf(
            Pair(3.3, 2.5), Pair(-210.0, -213.0), Pair(190.0, 180.1)
        )
        val correctLocation: List<String> = listOf("3.3,2.5", "-90.0,-180.0", "90.0,180.0")
        for (i in locations.indices) {
            assertEquals(Location(locations[i].first, locations[i].second).toString(), correctLocation[i])
        }
    }
}