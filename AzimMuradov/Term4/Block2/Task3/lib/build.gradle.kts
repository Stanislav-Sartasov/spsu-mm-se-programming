plugins {
    kotlin("jvm")

    id("org.jetbrains.kotlinx.kover") version "0.6.1"
}

dependencies {
    testImplementation(kotlin("test"))
    testImplementation("org.junit.jupiter:junit-jupiter:5.9.2")
}

tasks.test {
    useJUnitPlatform()
}
