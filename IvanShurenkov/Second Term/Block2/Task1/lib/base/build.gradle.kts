import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    kotlin("jvm") version "1.6.10"
    id("org.jetbrains.kotlinx.kover") version "0.5.0"
    //kotlin("jvm") version "1.6.10"
}

repositories {
    mavenCentral()
}

dependencies {
    implementation("org.jetbrains.kotlinx:kover:0.5.0")
    implementation("org.jetbrains.kotlin:kotlin-gradle-plugin:1.6.10")

    testImplementation(kotlin("test"))

    testImplementation("org.junit.jupiter:junit-jupiter-params:5.8.2")
    testImplementation("org.jetbrains.kotlin:kotlin-test:1.6.0")
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

tasks.test {
    useJUnitPlatform()
}

tasks.withType<KotlinCompile> {
    kotlinOptions.jvmTarget = "1.8"
}

// koverReport koverVerify
