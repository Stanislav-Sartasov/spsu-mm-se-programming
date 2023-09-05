import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    kotlin("jvm") version "1.6.20"
    kotlin("plugin.serialization") version "1.6.10"
    id("org.jetbrains.kotlinx.kover") version "0.5.0"
}

group = "org.example"
version = "1.0-SNAPSHOT"

dependencies {
    implementation("io.ktor:ktor-client-cio:2.0.1")
    implementation("io.ktor:ktor-client-core:2.0.1")
    implementation("org.jetbrains.kotlinx:kover:0.5.0")
    implementation("io.ktor:ktor-client-content-negotiation:2.0.1")
    implementation("io.ktor:ktor-serialization-kotlinx-json:2.0.1")
    implementation("org.jetbrains.kotlinx:kotlinx-serialization-json:1.3.2")

    implementation(project(":weather"))

    testImplementation(kotlin("test"))
//    testImplementation("io.mockk:mockk:1.12.3")
    testImplementation("io.ktor:ktor-client-mock:2.0.1")
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
            minValue = 70
        }
    }
}

tasks.withType<KotlinCompile> {
    kotlinOptions.jvmTarget = "1.8"
}