import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    kotlin("jvm") version "1.6.10"
    application
}

repositories {
    mavenCentral()
}

dependencies {
    implementation(kotlin("reflect"))
    implementation(files(projectDir.resolve(relative = "src/main/resources/base.jar").absolutePath))
}


val copyLibs by tasks.registering {
    setDependsOn(listOf(":lib:base:jar"))

    doFirst {
        tasks.findByPath(":lib:base:jar")!!.outputs.files.files.first().copyTo(
            target = projectDir.resolve(relative = "src/main/resources/base.jar"),
            overwrite = true
        )
    }
}

tasks.withType<KotlinCompile> {
    dependsOn(copyLibs)
    kotlinOptions.jvmTarget = "1.8"
}


application {
    mainClass.set("casino.app.MainKt")
}
