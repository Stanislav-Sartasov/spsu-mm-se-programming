plugins {
    kotlin("jvm") version "1.8.0"
    application
}

val mainClassName = "MainKt"
val MPJHome = System.getenv("MPJ_HOME") ?: error("MPJ_HOME not set")
val MPJStarterJar = files("$MPJHome/lib/starter.jar")
val MPJJar = files("$MPJHome/lib/mpj.jar")
val MPJClassPath = sourceSets["main"].runtimeClasspath - MPJJar

val numberOfProcesses = project.properties["numProcesses"] as? String? ?: "1"
val inputFilename = project.properties["inputFilename"] as? String? ?: ""
val outputFilename = project.properties["outputFilename"] as? String? ?: ""

repositories {
    mavenCentral()
}

dependencies {
    testImplementation(kotlin("test"))
    implementation(MPJJar)
}

kotlin {
    jvmToolchain(17)
}

application {
    mainClass.set("MainKt")
}

java {
    toolchain {
        languageVersion.set(JavaLanguageVersion.of(17))
    }
}


tasks.withType<JavaExec> {
    mainClass.set("runtime.starter.MPJRun")
    classpath = MPJStarterJar
    args = listOf(
        mainClassName,
        "-cp", MPJClassPath.asPath,
        "-np", numberOfProcesses,
        inputFilename, outputFilename
    )
    dependsOn("classes")
}