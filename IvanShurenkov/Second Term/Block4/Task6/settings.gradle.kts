rootProject.name = "Task6.kt"

include(":app-tornadofx")
include(":app-compose-desktop")
include(":lib:weather")
include(":lib:tomorrow")
include(":lib:stormglass")

pluginManagement {
    repositories {
        gradlePluginPortal()
        maven("https://maven.pkg.jetbrains.space/public/p/compose/dev")
    }
}