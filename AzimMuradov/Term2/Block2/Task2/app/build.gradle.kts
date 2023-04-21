import org.jetbrains.kotlin.gradle.tasks.KotlinCompile
import java.io.File.separatorChar as sep

plugins {
    kotlin("jvm") version "1.6.10"
    application
}

repositories {
    mavenCentral()
}

val libsNames = listOf(
    "base",
    "bot-basic",
    "bot-hi-lo",
    "bot-simple"
)

dependencies {
    for (libName in libsNames) {
        implementation(files(projectDir.resolve(relative = "src${sep}main${sep}resources$sep$libName.jar").absolutePath))
    }
}


val copyLibs by tasks.registering {
    setDependsOn(libsNames.map { libName -> ":lib:$libName:jar" })

    doFirst {
        for (libName in libsNames) {
            tasks.findByPath(":lib:$libName:jar")!!.outputs.files.files.first().copyTo(
                target = projectDir.resolve(relative = "src${sep}main${sep}resources$sep$libName.jar"),
                overwrite = true
            )
        }
    }
}

tasks.withType<KotlinCompile> {
    dependsOn(copyLibs)
    kotlinOptions.jvmTarget = "1.8"
}


application {
    mainClass.set("casino.app.MainKt")
}
