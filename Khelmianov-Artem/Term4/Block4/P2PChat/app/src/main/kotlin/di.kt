import domain.ChatServerInterface
import net.Server
import org.koin.core.module.dsl.bind
import org.koin.core.module.dsl.withOptions
import org.koin.dsl.module

fun chatServiceModule() = module {
    single { Server() } withOptions {
        bind<ChatServerInterface>()
    }
}