plugins {
    kotlin("jvm")
    id("io.ktor.plugin") version "2.3.0"
    application
}

dependencies {
    implementation(project(":core"))

    implementation("io.ktor:ktor-server-core-jvm:2.2.4")
    implementation("io.ktor:ktor-server-netty-jvm:2.2.4")
    implementation("ch.qos.logback:logback-classic:1.2.11")
}


java {
    toolchain {
        languageVersion.set(JavaLanguageVersion.of(11))
    }
}

application {
    mainClass.set("deanery.server.AppKt")

    val isDevelopment: Boolean = project.ext.has("development")
    applicationDefaultJvmArgs = listOf("-Dio.ktor.development=$isDevelopment")
}

ktor {
    docker {
        localImageName.set("deanery-server")
    }
}
