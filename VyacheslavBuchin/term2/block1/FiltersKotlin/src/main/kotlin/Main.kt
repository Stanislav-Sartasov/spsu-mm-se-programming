
fun main(args: Array<String>) {
	FilterCLI().apply {
		parseArgs(args)
		run()
	}
}