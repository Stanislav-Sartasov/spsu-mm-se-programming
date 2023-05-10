package chat.presentation.state

import chat.data.models.UserName


sealed interface User {

    object Me : User

    data class NotMe(val name: UserName) : User
}
