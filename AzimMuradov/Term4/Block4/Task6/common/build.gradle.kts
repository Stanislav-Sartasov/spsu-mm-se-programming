plugins {
    kotlin("jvm")
    kotlin("plugin.serialization") version "1.8.10"
    id("org.jetbrains.kotlinx.kover") version "0.6.1"
}

dependencies {
    implementation("org.jetbrains.kotlinx:kotlinx-serialization-json:1.5.0")

    implementation("io.github.microutils:kotlin-logging-jvm:3.0.5")
    implementation("org.slf4j:slf4j-log4j12:2.0.7")

    testImplementation(kotlin("test"))
    testImplementation("org.junit.jupiter:junit-jupiter:5.9.3")
}


tasks.test {
    useJUnitPlatform()
}

kover {
    filters {
        classes {
            excludes += "chat.data.*"
            excludes += "chat.serializers.InetSocketAddressSurrogate*"
        }
    }
}
