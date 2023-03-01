plugins {
    kotlin("jvm")

    application
}


val appMainClass = "psrs.app.AppKt"

val mpjHome = System.getenv("MPJ_HOME") ?: error("Specify `MPJ_HOME` environment variable")
val mpjStarterJar = files("$mpjHome/lib/starter.jar")
val mpjJar = files("$mpjHome/lib/mpj.jar")
val mpjClassPath = sourceSets.main.get().runtimeClasspath - mpjJar


// Parameters

val numberOfProcesses = 5
val inputPath = ""
val outputPath = ""


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
    args = listOf(appMainClass) + listOf("-cp", mpjClassPath.asPath) +
        listOf("-np", "$numberOfProcesses") +
        listOfNotNull(inputPath.takeUnless { it.isBlank() }) +
        listOfNotNull(outputPath.takeUnless { it.isBlank() })
    dependsOn("classes")
}
