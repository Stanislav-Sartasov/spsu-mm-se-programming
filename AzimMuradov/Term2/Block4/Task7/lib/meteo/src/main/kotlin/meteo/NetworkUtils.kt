package meteo

import kotlinx.coroutines.future.await
import kotlinx.serialization.decodeFromString
import kotlinx.serialization.json.Json
import meteo.presentation.state.LoadingState
import java.net.http.*

suspend inline fun <reified T> HttpClient.send(
    request: HttpRequest,
    json: Json,
    convertErrorBody: (String) -> String?,
): Result<T> = try {
    sendAsync(request, HttpResponse.BodyHandlers.ofString())
        .await()
        .toResult(convertErrorBody)
        .map { responseBody -> json.decodeFromString(responseBody) }
} catch (e: Throwable) {
    Result.failure(e)
}

inline fun <T> HttpResponse<T>.toResult(convertErrorBody: (String) -> String?): Result<T> = try {
    if (statusCode() in 200 until 300) {
        Result.success(body())
    } else {
        Result.failure(Exception(convertErrorBody(body().toString())))
    }
} catch (e: Throwable) {
    Result.failure(e)
}

fun <T> Result<T>.toLoadingState(): LoadingState<T> = fold(
    onSuccess = { value ->
        LoadingState.Success(value)
    },
    onFailure = { e ->
        LoadingState.Error(e.message.takeUnless(String?::isNullOrBlank) ?: "Unexpected error")
    }
)
