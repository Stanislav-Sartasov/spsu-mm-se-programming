package meteo

import io.mockk.every
import io.mockk.mockk
import kotlinx.coroutines.ExperimentalCoroutinesApi
import kotlinx.coroutines.test.runTest
import kotlinx.serialization.json.Json
import meteo.presentation.state.LoadingState
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import java.net.http.*
import java.util.concurrent.*
import kotlin.test.assertEquals

internal class NetworkUtilsTest {

    @OptIn(ExperimentalCoroutinesApi::class)
    @ParameterizedTest
    @MethodSource("stringHttpResponsesWithResults")
    fun `send HTTP request and get result`(stringHttpResponseWithResult: Triple<Int, () -> String, Result<Int>>) =
        runTest {
            val (code, bodyLambda, result) = stringHttpResponseWithResult

            val mockedRequest = mockk<HttpRequest>()
            val mockedResponse = mockk<HttpResponse<String>>()

            every { mockedResponse.statusCode() } returns code
            every { mockedResponse.body() } answers { bodyLambda() }

            val mockedClient = mockk<HttpClient>()

            every {
                mockedClient.sendAsync<String>(any(), any())
            } returns CompletableFuture.completedFuture(mockedResponse)

            assertEquals(
                expected = result.toString(),
                actual = mockedClient.send<Int>(mockedRequest, Json) { "error" }.toString(),
            )
        }

    @OptIn(ExperimentalCoroutinesApi::class)
    @Test
    fun `send HTTP request and get Failure with failed future`() = runTest {
        val mockedRequest = mockk<HttpRequest>()
        val mockedClient = mockk<HttpClient>()

        every {
            mockedClient.sendAsync<String>(any(), any())
        } returns CompletableFuture.failedFuture(Exception("error"))

        assertEquals(
            expected = Result.failure<Int>(Exception("error")).toString(),
            actual = mockedClient.send<Int>(mockedRequest, Json) { "error" }.toString(),
        )
    }

    @ParameterizedTest
    @MethodSource("intHttpResponsesWithResults")
    fun `convert HTTP response to result`(intHttpResponseWithResult: Triple<Int, () -> Int, Result<Int>>) {
        val (code, bodyLambda, result) = intHttpResponseWithResult

        val mockedResponse = mockk<HttpResponse<Int>>()

        every { mockedResponse.statusCode() } returns code
        every { mockedResponse.body() } answers { bodyLambda() }

        assertEquals(
            expected = result.toString(),
            actual = mockedResponse.toResult { "error" }.toString()
        )
    }

    @ParameterizedTest
    @MethodSource("resultsWithLoadingStates")
    fun `convert result to loading state`(resultWithLoadingState: Pair<Result<String>, LoadingState<String>>) {
        val (result, dataWithState) = resultWithLoadingState
        assertEquals(expected = dataWithState, actual = result.toLoadingState())
    }


    private companion object {

        // `HttpResponse` presented by its status code and body

        @JvmStatic
        fun intHttpResponsesWithResults() = listOf(
            Triple(200, { 42 }, Result.success(value = 42)),
            Triple(202, { 42 }, Result.success(value = 42)),
            Triple(100, { 42 }, Result.failure(Exception("error"))),
            Triple(300, { 42 }, Result.failure(Exception("error"))),
            Triple(400, { 42 }, Result.failure(Exception("error"))),
            Triple(400, { throw Exception("unexpected error") }, Result.failure(Exception("unexpected error"))),
        )

        @JvmStatic
        fun stringHttpResponsesWithResults() = listOf(
            Triple(200, { "42" }, Result.success(value = 42)),
            Triple(202, { "42" }, Result.success(value = 42)),
            Triple(100, { "42" }, Result.failure(Exception("error"))),
            Triple(300, { "42" }, Result.failure(Exception("error"))),
            Triple(400, { "42" }, Result.failure(Exception("error"))),
            Triple(400, { throw Exception("unexpected error") }, Result.failure(Exception("unexpected error"))),
        )

        @JvmStatic
        fun resultsWithLoadingStates() = listOf(
            Result.success(value = "abc") to LoadingState.Success(value = "abc"),
            Result.failure<String>(Exception()) to LoadingState.Error(message = "Unexpected error"),
            Result.failure<String>(Exception("")) to LoadingState.Error(message = "Unexpected error"),
            Result.failure<String>(Exception("  ")) to LoadingState.Error(message = "Unexpected error"),
            Result.failure<String>(Exception("error")) to LoadingState.Error(message = "error"),
        )
    }
}
