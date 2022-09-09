package textUtilities

object TextUtilities {

	fun trimQuotes(arg: String): String {
		if (arg.isNotEmpty() && arg.first() == '"' && arg.last() == '"') {
			return arg.slice(1 until arg.length - 1)
		}
		return arg
	}

	fun isValuable(arg: String) =
		arg.isEmpty() || arg.first() == '"' && arg.last() == '"' || arg.indexOf(' ') == -1

}