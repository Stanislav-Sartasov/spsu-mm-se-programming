package chat.app.state

import chat.data.Username


sealed interface User {

    object Me : User

    data class NotMe(val username: Username) : User
}
