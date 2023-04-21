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
    implementation("org.junit.jupiter:junit-jupiter:5.8.1")
    testImplementation(kotlin("test"))
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
