package meteo.app.presentation

import meteo.domain.entity.Weather
import meteo.ln
import meteo.presentation.state.*
import org.junit.jupiter.api.*
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments.arguments
import org.junit.jupiter.params.provider.MethodSource
import java.io.ByteArrayOutputStream
import java.io.PrintStream
import kotlin.test.assertEquals

internal class MeteoCliViewTest {

    private val byteArrayOutputStream = ByteArrayOutputStream()

    private val byteArrayErrorStream = ByteArrayOutputStream()

    private lateinit var outputStream: PrintStream

    private lateinit var errorStream: PrintStream

    private lateinit var view: MeteoCliView

    @BeforeEach
    fun setUp() {
        outputStream = PrintStream(byteArrayOutputStream, true)
        errorStream = PrintStream(byteArrayErrorStream, true)
        view = MeteoCliView(outputStream, errorStream)
    }

    @AfterEach
    fun tearDown() {
        byteArrayOutputStream.reset()
        byteArrayOutputStream.reset()
    }


    @ParameterizedTest
    @MethodSource("statesWithTexts")
    fun `render one state`(state: MeteoState, output: String, errors: String) {
        view.render(state)

        assertEquals(expected = output, actual = byteArrayOutputStream.toString())
        assertEquals(expected = errors, actual = byteArrayErrorStream.toString())
    }

    @Test
    fun `render two different states`() {
        view.render(
            MeteoState.Initialised(
                listOf(
                    NamedValue(
                        name = "service",
                        value = LoadingState.Loading
                    )
                )
            )
        )
        view.render(
            MeteoState.Initialised(
                listOf(
                    NamedValue(
                        name = "service",
                        value = LoadingState.Error("error")
                    )
                )
            )
        )

        assertEquals(
            expected = "Сервис service: Загрузка...${System.lineSeparator()}${System.lineSeparator()}",
            actual = byteArrayOutputStream.toString()
        )
        assertEquals(
            expected = "Сервис service: Ошибка \"error\"${System.lineSeparator()}${System.lineSeparator()}",
            actual = byteArrayErrorStream.toString()
        )
    }

    @Test
    fun `render two identical states`() {
        view.render(
            MeteoState.Initialised(
                listOf(
                    NamedValue(
                        name = "service",
                        value = LoadingState.Loading
                    )
                )
            )
        )
        view.render(
            MeteoState.Initialised(
                listOf(
                    NamedValue(
                        name = "service",
                        value = LoadingState.Loading
                    )
                )
            )
        )

        assertEquals(
            expected = "Сервис service: Загрузка...${System.lineSeparator()}${System.lineSeparator()}",
            actual = byteArrayOutputStream.toString()
        )
        assertEquals(
            expected = "",
            actual = byteArrayErrorStream.toString()
        )
    }


    private companion object {

        @JvmStatic
        fun statesWithTexts() = listOf(
            Triple(first = MeteoState.Uninitialised, second = "", third = ""),
            Triple(
                first = MeteoState.Initialised(
                    listOf(
                        NamedValue(
                            name = "service",
                            value = LoadingState.Loading
                        )
                    )
                ),
                second = "Сервис service: Загрузка...${System.lineSeparator()}${System.lineSeparator()}",
                third = ""
            ),
            Triple(
                first = MeteoState.Initialised(
                    listOf(
                        NamedValue(
                            name = "service",
                            value = LoadingState.Success(
                                Weather(
                                    description = null,
                                    temperature = null,
                                    cloudCoverage = null,
                                    humidity = null,
                                    precipitation = null,
                                    windDirection = null,
                                    windSpeed = null,
                                )
                            )
                        )
                    )
                ),
                second = buildString {
                    append("Сервис service:".ln())
                    append("  погода            : Данных нет".ln())
                    append("  температура       : Данных нет".ln())
                    append("  облачность        : Данных нет".ln())
                    append("  влажность         : Данных нет".ln())
                    append("  осадки            : Данных нет".ln())
                    append("  направление ветра : Данных нет".ln())
                    append("  скорость ветра    : Данных нет".ln())
                    append(System.lineSeparator())
                },
                third = ""
            ),
            Triple(
                first = MeteoState.Initialised(
                    listOf(
                        NamedValue(
                            name = "service",
                            value = LoadingState.Error("error")
                        )
                    )
                ),
                second = "",
                third = "Сервис service: Ошибка \"error\"${System.lineSeparator()}${System.lineSeparator()}"
            ),
        ).map { (a, b, c) -> arguments(a, b, c) }
    }
}
