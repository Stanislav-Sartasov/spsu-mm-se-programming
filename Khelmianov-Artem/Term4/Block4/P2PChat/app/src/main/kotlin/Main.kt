import kotlinx.coroutines.runBlocking
import org.koin.core.context.GlobalContext.startKoin
import org.koin.core.logger.PrintLogger


fun main(args: Array<String>) = runBlocking {
    startKoin {
        logger(PrintLogger())
        modules(chatServiceModule())
    }
    ui.main()
}
