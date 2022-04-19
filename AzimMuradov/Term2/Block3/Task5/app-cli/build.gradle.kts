import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    kotlin("jvm") version "1.6.10"
    id("org.jetbrains.kotlinx.kover") version "0.5.0"
    application
}

repositories {
    mavenCentral()
}

dependencies {
    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-jdk8:1.6.0")
    implementation("org.jetbrains.kotlinx:kotlinx-serialization-json:1.3.2")

    implementation(project(":lib:meteo"))
    implementation(project(":lib:meteo-open-weather"))
    implementation(project(":lib:meteo-storm-glass"))

    testImplementation(kotlin("test"))
    testImplementation("org.junit.jupiter:junit-jupiter:5.8.2")
    testImplementation("org.jetbrains.kotlinx:kotlinx-coroutines-test:1.6.1")
    testImplementation("app.cash.turbine:turbine:0.7.0")
    testImplementation("io.mockk:mockk:1.12.3")
}


tasks.withType<KotlinCompile> {
    kotlinOptions.jvmTarget = "1.8"
    kotlinOptions.freeCompilerArgs += "-opt-in=kotlin.RequiresOptIn"
}

tasks.test {
    useJUnitPlatform()
}

tasks.koverMergedHtmlReport {
    excludes = listOf("meteo.app.MainKt*")
}


application {
    mainClass.set("meteo.app.MainKt")
}

tasks.run.configure {
    standardInput = System.`in`
    standardOutput = System.out
    errorOutput = System.err
}
