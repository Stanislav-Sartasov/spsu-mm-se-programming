package service.weather.request

import entity.Location
import json.provider.JSONProvider

abstract class WeatherServiceHTTPRequest (protected val location: Location) : JSONProvider