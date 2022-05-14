package meteo.presentation.mvi

interface MviView<in State> {

    fun render(state: State)
}
