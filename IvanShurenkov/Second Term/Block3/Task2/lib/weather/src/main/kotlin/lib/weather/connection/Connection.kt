package lib.weather.connection

import org.json.JSONObject
import java.io.BufferedReader
import java.io.InputStreamReader
import java.net.HttpURLConnection
import java.net.URL

class Connection(_url: String) {
    private var connection: HttpURLConnection
    private var responseCode: Int = 0

    init {
        connection = URL(_url).openConnection() as HttpURLConnection
    }

    fun setConnection(newConnection: HttpURLConnection) {
        connection.disconnect()
        connection = newConnection
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
        } catch (e: Exception) {
            println(e.toString())
            e.toString()
        }
        //return "200"
    }

    fun getResponseInJSON(): JSONObject {
        if (responseCode != 200)
            return JSONObject("{\"error\":\"$responseCode\"}")
        val input = BufferedReader(InputStreamReader(connection.inputStream))
        var inputLine: String?
        val response = StringBuffer()

        while (input.readLine().also { inputLine = it } != null) {
            response.append(inputLine)
        }
        input.close()
        try {
            return JSONObject(response.toString())
        } catch (e: Exception) {
            if (responseCode != 200)
                return JSONObject("{\"error\": \"$responseCode\"}")
            else
                return JSONObject("{\"error\": \"${e.toString()}\"}")
        }
    }

    fun disconect() {
        connection.disconnect()
    }
}