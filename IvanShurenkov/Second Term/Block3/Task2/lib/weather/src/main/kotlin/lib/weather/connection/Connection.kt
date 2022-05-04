package lib.weather.connection

import org.json.JSONObject
import java.io.BufferedReader
import java.io.InputStreamReader
import java.net.HttpURLConnection
import java.net.URL
import java.net.UnknownHostException

class Connection(_url: String) {
    private val connection: HttpURLConnection
    private val url: URL
    private var responseCode: Int = 0

    init {
        url = URL(_url)
        connection = url.openConnection() as HttpURLConnection
    }

    fun setAuthorizationHeader(apikey: String) {
        connection.setRequestProperty("Authorization", apikey)
    }

    fun requestGet(): String {
        return try {
            connection.requestMethod = "GET"
            val returnCode = connection.responseCode.toString()
            if (returnCode != "200") {
                println("Response code: $returnCode")
            }
            responseCode = returnCode.toInt()
            returnCode
        } catch (e: UnknownHostException) {
            println("Unknow host")
            "Unknow host"
        }
        //return "200"
    }

    fun getResponseInJSON(): JSONObject {
        if (responseCode != 200)
            return JSONObject("{\"error\": \"$responseCode\"}")
        val input = BufferedReader(InputStreamReader(connection.inputStream))
        var inputLine: String?
        val response = StringBuffer()

        while (input.readLine().also { inputLine = it } != null) {
            response.append(inputLine)
        }
        input.close()
        val jsonObject = JSONObject(response.toString())
        return jsonObject
    }

    fun disconect() {
        connection.disconnect()
    }
}