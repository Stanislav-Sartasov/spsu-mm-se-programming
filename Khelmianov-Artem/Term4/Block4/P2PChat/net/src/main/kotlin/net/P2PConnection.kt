package net

import domain.User
import io.ktor.network.sockets.Connection

internal data class P2PConnection(
    val user: User,
    val connection: Connection
)
