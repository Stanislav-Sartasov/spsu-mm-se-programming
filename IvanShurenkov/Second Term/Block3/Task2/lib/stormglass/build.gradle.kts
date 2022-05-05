import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    kotlin("jvm") version "1.6.10"
    id("org.jetbrains.kotlinx.kover") version "0.5.0"
}

repositories {
    mavenCentral()
}

dependencies {
    implementation(project(":lib:weather"))
    implementation("org.jetbrains.kotlinx:kover:0.5.0")
    implementation("org.jetbrains.kotlin:kotlin-gradle-plugin:1.6.10")
    implementation("org.json:json:20220320")

    testImplementation(kotlin("test"))

    testImplementation("org.junit.jupiter:junit-jupiter-params:5.8.2")
    testImplementation("org.jetbrains.kotlin:kotlin-test:1.6.0")
    testImplementation("io.mockk:mockk:1.12.3")
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

// koverReport koverVerify
