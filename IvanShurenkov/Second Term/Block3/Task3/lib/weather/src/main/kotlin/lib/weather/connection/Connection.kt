package lib.weather.connection

import lib.weather.stream.ErrorStream
import org.json.JSONObject
import org.kodein.di.*
import java.io.BufferedReader
import java.io.InputStreamReader
import java.net.HttpURLConnection

class Connection(private val connection: HttpURLConnection, private val stream: DI): IConnection {
    private var responseCode: Int = 0
    private val errorStream: ErrorStream by stream.instance()

    override fun setAuthorizationHeader(apikey: String) {
        connection.setRequestProperty("Authorization", apikey)
    }

    override fun requestGet(): String {
        return try {
            connection.requestMethod = "GET"
            val returnCode = connection.responseCode.toString()
            if (returnCode != "200") {
                errorStream.print("Response code: $returnCode")
            }
            responseCode = returnCode.toInt()
            returnCode
        } catch (e: Exception) {
            errorStream.print(e.toString())
            responseCode = -1
            e.toString()
        }
        //return "200"
    }

    override fun getResponseInJSON(): JSONObject {
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
            return JSONObject("{\"error\": \"${e.toString()}\"}")
        }
    }

    override fun disconect() {
        connection.disconnect()
    }
}