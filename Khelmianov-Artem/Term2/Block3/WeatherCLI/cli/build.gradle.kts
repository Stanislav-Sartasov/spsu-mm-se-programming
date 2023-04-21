import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    kotlin("jvm") version "1.6.20"
    kotlin("plugin.serialization") version "1.6.10"
    application
}

group = "org.example"
version = "1.0-SNAPSHOT"

dependencies {
    implementation("io.ktor:ktor-client-cio:2.0.1")
    implementation("io.ktor:ktor-client-core:2.0.1")
    implementation("io.ktor:ktor-client-content-negotiation:2.0.1")
    implementation("io.ktor:ktor-serialization-kotlinx-json:2.0.1")
    implementation("org.jetbrains.kotlinx:kotlinx-serialization-json:1.3.2")

    implementation(project(":weather"))
    implementation(project(":openweather"))
    implementation(project(":weatherbit"))

    testImplementation(kotlin("test"))
}

tasks.test {
    useJUnitPlatform()
}

tasks.withType<KotlinCompile> {
    kotlinOptions.jvmTarget = "1.8"
}

application {
    mainClass.set("MainKt")
}

tasks.run.configure {
    standardInput = System.`in`
    standardOutput = System.out
    errorOutput = System.err
}