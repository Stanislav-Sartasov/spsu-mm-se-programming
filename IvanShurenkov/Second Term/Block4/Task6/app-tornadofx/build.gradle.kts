import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    kotlin("jvm") version "1.6.10"
    id("org.jetbrains.kotlinx.kover") version "0.5.0"
    application
    id("org.openjfx.javafxplugin") version "0.0.12"
}

repositories {
    mavenCentral()
}

dependencies {
    implementation("no.tornado:tornadofx:1.7.20")

    implementation(project(":lib:weather"))
    implementation(project(":lib:tomorrow"))
    implementation(project(":lib:stormglass"))

    implementation("org.jetbrains.kotlinx:kover:0.5.0")
    implementation("org.jetbrains.kotlin:kotlin-gradle-plugin:1.6.10")
    implementation("org.json:json:20220320")
    implementation("org.kodein.di:kodein-di:7.11.0")
    implementation("org.kodein.di:kodein-di-framework-tornadofx-jvm:7.11.0")
    implementation(kotlin("reflect"))

    testImplementation(kotlin("test"))

    testImplementation("org.junit.jupiter:junit-jupiter-params:5.8.2")
    testImplementation("org.jetbrains.kotlin:kotlin-test:1.6.0")
    testImplementation("org.kodein.di:kodein-di:7.11.0")
}

tasks.koverHtmlReport {
    isEnabled = true
    htmlReportDir.set(layout.buildDirectory.dir("kover-report/html-result"))
}

tasks.koverVerify {
    rule {
        name = "Minimal line coverage rate in percent"
        bound {
            minValue = 80
        }
    }
}

tasks.test {
    useJUnitPlatform()
}

tasks.withType<KotlinCompile> {
    kotlinOptions.jvmTarget = "1.8"
}

application {
    mainClass.set("meteo.app.MainKt")
}

javafx {
    version = "11.0.2"
    modules = listOf("javafx.controls")
}

// koverReport koverVerify
