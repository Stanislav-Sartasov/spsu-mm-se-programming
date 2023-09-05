package domain

import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.StateFlow

interface ChatServerInterface : AutoCloseable {
    val user: User
    val connectedUsers: StateFlow<List<User>>
    val receivedMessages: Flow<Message>

    fun start(user: User, port: Int): Result<Int>
    suspend fun connect(ip: String, port: Int): Result<Unit>
    suspend fun broadcastMessage(message: Message)
}