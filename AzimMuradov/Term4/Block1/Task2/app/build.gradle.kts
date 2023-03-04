plugins {
    kotlin("jvm")

    application
}

val appMainClass = "psrs.app.AppKt"

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

dependencies {
    implementation(project(":lib"))
    implementation(mpjJar)
}

java {
    toolchain {
        languageVersion.set(JavaLanguageVersion.of(8))
    }
}

tasks.withType<JavaExec> {
    mainClass.set("runtime.starter.MPJRun")
    classpath = mpjStarterJar
    args = listOf(appMainClass) + listOf("-cp", mpjClassPath.asPath) + listOf("-np", numberOfProcesses) +
        inputFilename + outputFilename
    dependsOn("classes")
}
