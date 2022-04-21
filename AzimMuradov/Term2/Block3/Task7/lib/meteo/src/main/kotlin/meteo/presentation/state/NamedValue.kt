package meteo.presentation.state

data class NamedValue<out T>(
    val name: String,
    val value: T,
)
