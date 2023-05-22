package chat.app.state


sealed interface State {

    object SplashScreen : State

    sealed interface LoginScreen : State {

        object Idle : LoginScreen

        data class Error(val message: String) : LoginScreen
    }

    sealed interface ChatScreen : State {

        object Alone : ChatScreen

        data class NotAlone(val messages: List<Message>) : ChatScreen
    }
}
