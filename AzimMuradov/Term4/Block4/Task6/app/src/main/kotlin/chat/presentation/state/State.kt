package chat.presentation.state

import chat.data.models.MessageData


sealed class State {

    object Splash : State()

    object MainMenu : State()

    data class ChatRoom(val messages: List<Pair<User, MessageData>>) : State()
}
