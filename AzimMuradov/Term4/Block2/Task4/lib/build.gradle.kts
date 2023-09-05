plugins {
    kotlin("jvm")
    id("org.jetbrains.kotlin.plugin.atomicfu") version "1.8.10"
    id("org.jetbrains.kotlinx.kover") version "0.6.1"
}

dependencies {
    testImplementation(kotlin("test"))
    testImplementation("org.junit.jupiter:junit-jupiter:5.9.2")
    testImplementation("org.jetbrains.kotlinx:atomicfu:0.20.0")
}

tasks.test {
    useJUnitPlatform()
}
