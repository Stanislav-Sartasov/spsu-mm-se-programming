plugins {
    kotlin("jvm") version "1.8.20"
    id("org.jetbrains.compose") version "1.4.0"
}

val koinVersion: String by project

dependencies {
    implementation(project(":net"))
    implementation(project(":domain"))

    implementation(compose.desktop.currentOs)
    implementation("io.insert-koin:koin-core:$koinVersion")
}

kotlin {
    jvmToolchain(19)
}

compose.desktop {
    application {
        mainClass = "MainKt"
    }
}
