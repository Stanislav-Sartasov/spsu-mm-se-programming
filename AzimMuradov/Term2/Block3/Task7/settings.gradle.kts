rootProject.name = "MeteoGUI"

include(":app-cli")
include(":app-compose-desktop")
include(":app-tornadofx")

include(":lib:meteo")
include(":lib:meteo-open-weather")
include(":lib:meteo-storm-glass")


pluginManagement {
    repositories {
        gradlePluginPortal()
        maven("https://maven.pkg.jetbrains.space/public/p/compose/dev")
    }
}
