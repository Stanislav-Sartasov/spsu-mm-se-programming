plugins {
    kotlin("jvm") version "1.8.0"
    application
}

val appMainClass = "floyd.MainKt"

val mpjHome = System.getenv("MPJ_HOME") ?: error("Specify `MPJ_HOME` environment variable")
val mpjStarterJar = files("$mpjHome/lib/starter.jar")
val mpjJar = files("$mpjHome/lib/mpj.jar")
val mpjClassPath = sourceSets.main.get().runtimeClasspath - mpjJar

val (numberOfProcesses, inputFilename, outputFilename) = (project.properties["args"] as? String? ?: "")
    .split(" ")
    .filterNot { it.isBlank() }
    .let {
        Triple(
            it.getOrElse(0) { "1" },
            listOfNotNull(it.getOrNull(1)),
            listOfNotNull(it.getOrNull(2))
        )
    }

repositories {
    mavenCentral()
}

dependencies {
    implementation(mpjJar)
}

java {
    toolchain {
        languageVersion.set(JavaLanguageVersion.of(8))
    }
}

tasks.run.configure {
    mainClass.set("runtime.starter.MPJRun")
    classpath = mpjStarterJar
    args = listOf(appMainClass) + listOf("-cp", mpjClassPath.asPath) +
            listOf("-np", numberOfProcesses) + listOf("-Xms1024m") + inputFilename + outputFilename
    jvmArgs = listOf("-Xms1024m")
    dependsOn("classes")
}