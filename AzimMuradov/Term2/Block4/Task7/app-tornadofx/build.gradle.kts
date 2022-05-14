import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    kotlin("jvm") version "1.6.20"
    id("org.jetbrains.kotlinx.kover") version "0.5.0"
    application
    id("org.openjfx.javafxplugin") version "0.0.12"
}

repositories {
    mavenCentral()
}

dependencies {
    implementation("no.tornado:tornadofx:1.7.20")

    implementation(kotlin("reflect"))
    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-jdk8:1.6.1")
    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-javafx:1.6.1")
    implementation("org.jetbrains.kotlinx:kotlinx-serialization-json:1.3.2")
    implementation("org.kodein.di:kodein-di-framework-tornadofx-jvm:7.11.0")

    implementation(project(":lib:meteo"))
    implementation(project(":lib:meteo-open-weather"))
    implementation(project(":lib:meteo-storm-glass"))

    testImplementation(kotlin("test"))
    testImplementation("org.junit.jupiter:junit-jupiter:5.8.2")
}


tasks.withType<KotlinCompile> {
    kotlinOptions.jvmTarget = "1.8"
    kotlinOptions.freeCompilerArgs += "-opt-in=kotlin.RequiresOptIn"
}

tasks.test {
    useJUnitPlatform()
}

tasks.koverMergedHtmlReport {
    includes = listOf("meteo.app.presentation.MeteoTornadoFxMessagesWizard")
}


application {
    mainClass.set("meteo.app.MainKt")
}

javafx {
    version = "11.0.2"
    modules = listOf("javafx.controls")
}
