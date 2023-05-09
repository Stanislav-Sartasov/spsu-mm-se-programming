package domain

import kotlinx.serialization.Serializable
import kotlin.random.Random

@Serializable
sealed class Message {
    val id: Int = Random.nextInt()

    @Serializable
    sealed class Service : Message() {
        @Serializable
        data class Ping(
            val from: User,
            val ip: String,
            val port: Int
        ) : Service()
    }

    @Serializable
    data class Normal(
        val from: String,
        val msg: String,
    ) : Message()
}

