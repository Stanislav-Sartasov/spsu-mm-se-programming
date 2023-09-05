import org.json.JSONObject
import org.junit.jupiter.api.Test
import java.io.File
import kotlin.test.assertEquals
import kotlin.test.assertFails

class TestIWeatherApi {
    @Test
    fun `Test search paramether`() {
        val jsonFiles = listOf("src/test/resources/tomorrow.json", "src/test/resources/stormglass.json")
        val correctAns = listOf(64.0, 87.16)
        for (i in jsonFiles.indices) {
            val jsonString = File(jsonFiles[i]).inputStream().readBytes().toString(Charsets.UTF_8)
            val jo = TestObject.searchParametherInJson("cloudCover", JSONObject(jsonString))
            if (jo is JSONObject)
                assertEquals(jo.get("sg").toString().toDouble(), correctAns[i])
            else
                assertFails { "Wasn't found field" }
        }
    }

    @Test
    fun `Test getURL`() {
        val url = "https://127.0.0.1:9000/test.json"
        val connection = TestObject.getURLConnection(url)
        assertEquals(connection.url.toString(), url)
    }
}