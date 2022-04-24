package meteo.app.presentation

import meteo.app.presentation.MeteoCliMessagesWizard.errorWithLoadingServiceMessage
import meteo.app.presentation.MeteoCliMessagesWizard.loadingServiceMessage
import meteo.app.presentation.MeteoCliMessagesWizard.toHumanReadable
import meteo.presentation.mvi.MviView
import meteo.presentation.state.LoadingState
import meteo.presentation.state.MeteoState
import java.io.PrintStream

class MeteoCliView(
    private val outputStream: PrintStream,
    private val errorStream: PrintStream,
) : MviView<MeteoState> {

    private var prevState: MeteoState = MeteoState.Uninitialised

    override fun render(state: MeteoState) {
        when (state) {
            MeteoState.Uninitialised -> Unit
            is MeteoState.Initialised -> prevState.let { prevState ->
                val weathersToPrint = when (prevState) {
                    MeteoState.Uninitialised -> state.weatherList
                    is MeteoState.Initialised -> (prevState.weatherList zip state.weatherList).filter { (prev, curr) ->
                        prev != curr
                    }.unzip().second
                }

                for ((name, weather) in weathersToPrint) {
                    when (weather) {
                        LoadingState.Loading -> outputStream.println(
                            loadingServiceMessage(name)
                        )
                        is LoadingState.Success -> outputStream.println(
                            weather.value.toHumanReadable(name)
                        )
                        is LoadingState.Error -> errorStream.println(
                            errorWithLoadingServiceMessage(name, weather.message)
                        )
                    }
                }
            }
        }

        prevState = state
    }
}
