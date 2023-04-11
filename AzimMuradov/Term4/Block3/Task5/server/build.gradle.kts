plugins {
    application
    kotlin("jvm")
    id("io.ktor.plugin") version "2.2.4"
}

application {
    mainClass.set("deanery.server.ApplicationKt")

    val isDevelopment: Boolean = project.ext.has("development")
    applicationDefaultJvmArgs = listOf("-Dio.ktor.development=$isDevelopment")
}

repositories {
    mavenCentral()
    maven { url = uri("https://maven.pkg.jetbrains.space/public/p/ktor/eap") }
}

dependencies {
    implementation(project(":core"))

    implementation("io.ktor:ktor-server-core-jvm:2.2.4")
    implementation("io.ktor:ktor-server-netty-jvm:2.2.4")
    implementation("ch.qos.logback:logback-classic:1.2.11")

    testImplementation("io.ktor:ktor-server-tests-jvm:2.2.4")
    testImplementation("org.jetbrains.kotlin:kotlin-test-junit:1.8.10")
}

//plugins {
//    kotlin("jvm")
//    id("org.jetbrains.kotlinx.kover") version "0.6.1"
//}
//
//dependencies {
//    implementation(project(":core"))
//
//    testImplementation(kotlin("test"))
//    testImplementation("org.junit.jupiter:junit-jupiter:5.9.2")
//}
//
//tasks.test {
//    useJUnitPlatform()
//}
