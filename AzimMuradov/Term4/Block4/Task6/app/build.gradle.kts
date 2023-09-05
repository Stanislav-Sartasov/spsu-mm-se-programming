plugins {
    kotlin("jvm")
    id("org.jetbrains.compose") version "1.4.0"
}

dependencies {
    implementation(project(":client"))
    implementation(project(":common"))

    implementation(compose.desktop.currentOs)

    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-core:1.7.0")

    implementation("io.github.microutils:kotlin-logging-jvm:3.0.5")
}


compose.desktop {
    application {
        mainClass = "chat.app.AppKt"
    }
}
