//plugins {
//    kotlin("jvm") version "1.5.21"
//    id("application")
//    id("org.openjfx.javafxplugin") version "0.0.8"
//}
//
//group = "org.example"
//version = "1.0-SNAPSHOT"
//
//repositories {
//    mavenCentral()
//}
//
//javafx {
//    modules("javafx.controls", "javafx.fxml", "javafx.graphics", "javafx.base")
//}
//
//dependencies {
//    testImplementation(kotlin("test"))
//    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-core:1.5.2")
//    implementation(kotlin("stdlib-jdk8"))
//    implementation("org.openjfx:javafx-controls:17")
//    implementation("org.openjfx:javafx-fxml:17")
//}
//
//
//
//tasks.test {
//    useJUnitPlatform()
//}
//
//kotlin {
//    jvmToolchain(8)
//}
//
//
//tasks.withType<org.jetbrains.kotlin.gradle.tasks.KotlinCompile> {
//    kotlinOptions {
//        jvmTarget = "1.8"
//    }
//}

import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    kotlin("jvm") version "1.6.20"
    application
    id("org.openjfx.javafxplugin") version "0.0.12"
}

repositories {
    mavenCentral()
}

dependencies {
//    implementation("no.tornado:tornadofx:1.7.20")
//
//    implementation(kotlin("reflect"))
//    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-jdk8:1.6.1")
//    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-javafx:1.6.1")
//    implementation("org.jetbrains.kotlinx:kotlinx-serialization-json:1.3.2")
//    implementation("org.kodein.di:kodein-di-framework-tornadofx-jvm:7.11.0")
//
//    testImplementation("org.junit.jupiter:junit-jupiter:5.8.2")

    testImplementation(kotlin("test"))
    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-core:1.5.2")
    implementation(kotlin("stdlib-jdk8"))
    implementation("org.openjfx:javafx-controls:17")
    implementation("org.openjfx:javafx-fxml:17")
}


tasks.withType<KotlinCompile> {
    kotlinOptions.jvmTarget = "1.8"
    kotlinOptions.freeCompilerArgs += "-opt-in=kotlin.RequiresOptIn"
}

//tasks.test {
//    useJUnitPlatform()
//}

application {
    mainClass.set("peerToPeerChat.ChatApp")
}

javafx {
    version = "11.0.2"
    modules = listOf("javafx.controls")
}
