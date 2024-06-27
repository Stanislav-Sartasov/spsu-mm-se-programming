plugins {
    kotlin("jvm") version "1.9.22"
    id("com.github.johnrengelman.shadow") version "7.1.2"
    application
}

group = "org.example"
version = "1.0-SNAPSHOT"

repositories {
    mavenCentral()
}

dependencies {
    testImplementation(kotlin("test"))
    implementation("org.jetbrains.kotlin:kotlin-reflect:1.5.10")

}

tasks.test {
    useJUnitPlatform()
}

kotlin {
    jvmToolchain(8)
}

application {
    mainClass.set("examsystem.MainKt")
}