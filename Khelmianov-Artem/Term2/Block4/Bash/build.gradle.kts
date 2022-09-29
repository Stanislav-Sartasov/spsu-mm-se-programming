import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    kotlin("jvm") version "1.6.20"
    id("org.jetbrains.kotlinx.kover") version "0.5.0"
    application
}

group = "org.example"
version = "1.0-SNAPSHOT"

repositories {
    mavenCentral()
}

dependencies {
    testImplementation(kotlin("test"))
    implementation("org.buildobjects:jproc:2.8.0")
    testImplementation("org.junit.jupiter:junit-jupiter:5.8.2")
    implementation("org.jetbrains.kotlinx:kover:0.6.0")
}

tasks.test {
    useJUnitPlatform()
}
tasks.koverHtmlReport {
    isEnabled = true
    htmlReportDir.set(layout.buildDirectory.dir("kover-report/html-result"))
}

tasks.koverVerify {
    rule {
        name = "Minimal line coverage rate in percent"
        bound {
            minValue = 75
        }
    }
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