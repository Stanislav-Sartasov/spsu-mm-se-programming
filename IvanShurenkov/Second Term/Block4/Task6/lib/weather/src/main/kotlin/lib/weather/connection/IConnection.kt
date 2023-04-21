package lib.weather.connection

import org.json.JSONObject

interface IConnection {
    fun setAuthorizationHeader(apikey: String)

    fun requestGet(): String

    fun getResponseInJSON(): JSONObject

    fun disconect()
}