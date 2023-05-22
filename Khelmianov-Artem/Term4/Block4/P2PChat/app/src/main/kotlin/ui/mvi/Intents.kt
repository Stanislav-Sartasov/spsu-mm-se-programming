package ui.mvi

import domain.Message

interface Intent

sealed interface ChatIntent : Intent {
    data class Start(val port: Int, val userName: String) : ChatIntent
    data class Connect(val ip: String, val port: Int) : ChatIntent
    data class SendMessage(val message: String) : ChatIntent
    data class ReceiveMessage(val message: Message) : ChatIntent
    object OnError : ChatIntent
    object OnClose : ChatIntent
}
