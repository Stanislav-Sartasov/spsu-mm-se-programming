package chat.app.state


sealed interface State {

    object SplashScreen : State

    object LoginScreen : State

    sealed interface ChatScreen : State {

        object Alone : ChatScreen

        data class NotAlone(val messages: List<Message>) : ChatScreen
    }
}
