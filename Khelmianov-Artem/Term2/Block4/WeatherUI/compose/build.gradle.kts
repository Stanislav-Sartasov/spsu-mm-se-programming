import org.jetbrains.compose.compose

plugins {
    kotlin("jvm") version "1.6.10"
    kotlin("plugin.serialization") version "1.6.10"
    id("org.jetbrains.compose") version "1.1.0"
}

dependencies {
    implementation("io.ktor:ktor-client-cio:2.0.1")
    implementation("io.ktor:ktor-client-core:2.0.1")
    implementation("io.ktor:ktor-client-content-negotiation:2.0.1")
    implementation("io.ktor:ktor-serialization-kotlinx-json:2.0.1")
    implementation("org.jetbrains.kotlinx:kotlinx-serialization-json:1.3.2")
    implementation(compose.desktop.currentOs)

    implementation(project(":weather"))
    implementation(project(":openweather"))
    implementation(project(":weatherbit"))
    implementation("io.insert-koin:koin-core:3.1.6")
}

compose.desktop {
    application {
        mainClass = "MainKt"
    }
}