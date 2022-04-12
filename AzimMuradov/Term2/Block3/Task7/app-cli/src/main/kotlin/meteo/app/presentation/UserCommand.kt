package meteo.app.presentation

sealed interface UserCommand {

    object Refresh : UserCommand

    object Help : UserCommand

    object Exit : UserCommand
}
