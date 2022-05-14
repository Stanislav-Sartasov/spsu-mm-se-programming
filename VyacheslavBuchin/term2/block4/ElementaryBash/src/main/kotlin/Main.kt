import command.*

fun main(args: Array<String>) {
	val wc = CatCommand()
	wc.execute(arrayOf())
	println(wc.output.read())
}