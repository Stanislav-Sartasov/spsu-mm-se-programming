package meteo.presentation.state

sealed interface LoadingState<out T> {

    object Loading : LoadingState<Nothing>

    data class Success<T>(val value: T) : LoadingState<T>

    data class Error(val message: String) : LoadingState<Nothing>
}
