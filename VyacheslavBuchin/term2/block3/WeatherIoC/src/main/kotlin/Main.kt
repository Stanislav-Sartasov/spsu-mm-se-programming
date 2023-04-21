import kodein.DIConfiguration
import org.kodein.di.DI
import org.kodein.di.instance

fun main(args: Array<String>) {
	val di = DI {
		import(DIConfiguration.default)
	}

	val app: WeatherAppCLI by di.instance(arg = args)

	app.run()
}
