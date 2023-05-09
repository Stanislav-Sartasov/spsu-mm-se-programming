plugins {
    kotlin("jvm")
    kotlin("plugin.serialization") version "1.8.10"
    id("org.jetbrains.kotlinx.kover") version "0.6.1"
}

dependencies {
    implementation("org.jetbrains.kotlinx:kotlinx-serialization-json:1.5.0")

    implementation("io.github.microutils:kotlin-logging-jvm:2.1.20")
    implementation("org.slf4j:slf4j-log4j12:2.0.7")
    implementation("org.apache.logging.log4j:log4j-slf4j-impl:2.17.1")
    implementation("org.apache.logging.log4j:log4j-api:2.17.1")
    implementation("org.apache.logging.log4j:log4j-core:2.17.1")

    testImplementation(kotlin("test"))
    testImplementation("org.junit.jupiter:junit-jupiter:5.9.2")
}


tasks.test {
    useJUnitPlatform()
}
