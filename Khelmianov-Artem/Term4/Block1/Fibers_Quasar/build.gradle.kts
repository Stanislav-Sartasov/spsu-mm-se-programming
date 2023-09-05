import org.gradle.internal.classpath.Instrumented.systemProperty

plugins {
    kotlin("jvm") version "1.8.0"
    application
}

group = "org.example"
version = "1.0-SNAPSHOT"

repositories {
    mavenCentral()
}

val quasar = configurations.create("quasar")

dependencies {
    testImplementation(kotlin("test"))
    quasar("co.paralleluniverse:quasar-core:0.8.0")
    quasar("co.paralleluniverse:quasar-kotlin:0.8.0")
    implementation(quasar)
}

tasks.test {
    useJUnitPlatform()
}

kotlin {
    jvmToolchain(11)
}

val quasarAgent = quasar.files.find { it.name.contains("quasar-core") }?.path

application {
    mainClass.set("MainKt")
    applicationDefaultJvmArgs = listOf(
        "-javaagent:${quasarAgent}",
        "--add-opens=java.base/java.lang=ALL-UNNAMED"   // suppress some warnings
    )
    systemProperty("co.paralleluniverse.fibers.verifyInstrumentation", "true")
    systemProperty("co.paralleluniverse.fibers.detectRunawayFibers", "false")
}
