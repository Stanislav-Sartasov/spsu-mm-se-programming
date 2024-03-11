package peerToPeerChat

import javafx.application.Application
import javafx.scene.Scene
import javafx.scene.control.Button
import javafx.scene.control.TextArea
import javafx.scene.control.TextField
import javafx.scene.layout.VBox
import javafx.stage.Stage
import java.net.InetSocketAddress

class ChatApp : Application() {

    private val chat by lazy { Chat(parameters.raw[0].toInt()) }

    override fun start(primaryStage: Stage) {
        val chatArea = TextArea()
        val messageInput = TextField()
        val connectButton = Button("Connect")
        val sendButton = Button("Send")

        // Event handler for the Connect button
        connectButton.setOnAction {
            val addressInput = getAddressInput()
            if (addressInput != null) {
                chat.connectTo(addressInput)
            }
        }

        // Event handler for the Send button
        sendButton.setOnAction {
            val message = messageInput.text
            chat.send(message)
        }

        // Set up the layout
        val layout = VBox(chatArea, messageInput, connectButton, sendButton)
        val scene = Scene(layout, 400.0, 300.0)

        // Event handler for closing the application
        primaryStage.setOnCloseRequest {
            chat.close()
        }

        primaryStage.title = "Peer-to-Peer Chat"
        primaryStage.scene = scene
        primaryStage.show()

        // Event handler for updating the chat area when a message is received
        chat.onMessage = { message ->
            chatArea.appendText("$message\n")
        }
    }

    private fun getAddressInput(): InetSocketAddress? {
        val addressString = getAddressStringFromUserInput() ?: return null

        return try {
            InetSocketAddress(addressString.split(":")[0], addressString.split(":")[1].toInt())

        } catch (e: Exception) {
            println("Invalid address format.")
            println("In ChatApp.getAddressInput")
            null
        }
    }

    private fun getAddressStringFromUserInput(): String? {
        return try {
            val input = showInputDialog("Enter IP address and port (e.g., 127.0.0.1:8080):")
            if (input.isNullOrBlank()) null else input.trim()
        } catch (e: Exception) {
            println(e.message)
            println("In ChatApp.getAddressStringFromUserInput")
            null
        }
    }

    private fun showInputDialog(prompt: String): String? {
        return javafx.scene.control.TextInputDialog().apply {
            contentText = prompt
        }.showAndWait().orElse(null)
    }

    companion object {
        @JvmStatic
        fun main(args: Array<String>) {
            //require(args.size == 1) { "Wrong number of arguments, expected 1" }

            val parsedArgs = args.map { it.toInt() }
            val port = parsedArgs[0]
            launch(ChatApp::class.java, *args)
        }
    }
}
