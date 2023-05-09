package chat

import chat.data.models.UserName
import kotlinx.coroutines.delay
import kotlinx.coroutines.runBlocking
import kotlin.random.Random
import kotlin.random.nextInt
import kotlin.time.Duration.Companion.hours
import kotlin.time.Duration.Companion.milliseconds


fun main() = runBlocking {
    val peer = Peer()

    val port = Random.nextInt(4000..5000)

    peer.run(
        hubIp = "127.0.0.1", hubPort = 12345,
        peerServerPort = port
    )

    delay(300.milliseconds)

    peer.intents.emit(Intent.JoinTheChat(user = UserName("User #$port")))

    delay(1000.milliseconds)

    peer.intents.emit(Intent.SendMessage(text = "User #$port > My awesome message"))

    delay(1.hours)
}
