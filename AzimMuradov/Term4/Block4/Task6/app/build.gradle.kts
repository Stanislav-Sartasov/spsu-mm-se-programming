plugins {
    kotlin("jvm")

    id("org.jetbrains.compose") version "1.4.0"
}

dependencies {
    implementation(project(":client"))
    implementation(project(":common"))

    implementation(compose.desktop.currentOs)

    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-core:1.6.4")

    implementation("io.github.microutils:kotlin-logging-jvm:2.1.20")
    implementation("org.slf4j:slf4j-log4j12:2.0.7")
    implementation("org.apache.logging.log4j:log4j-slf4j-impl:2.17.1")
    implementation("org.apache.logging.log4j:log4j-api:2.17.1")
    implementation("org.apache.logging.log4j:log4j-core:2.17.1")
}


compose.desktop {
    application {
        mainClass = "chat.app.AppKt"
    }
}
