package chat.hub


fun main(args: Array<String>) = Hub().run(port = args.single().toInt())
