package chat.app.state


sealed class State {

    object SplashScreen : State()

    object LoginScreen : State()

    data class ChatScreen(val messages: List<Message>) : State()
}
