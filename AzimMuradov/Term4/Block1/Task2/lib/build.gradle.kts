import java.io.File.separatorChar as sep

plugins {
    kotlin("jvm")
}

val mpjHome = System.getenv("MPJ_HOME") ?: error("Specify `MPJ_HOME` environment variable")
val mpjJar = files("$mpjHome${sep}lib${sep}mpj.jar")

dependencies {
    implementation(mpjJar)
}

java {
    toolchain {
        languageVersion.set(JavaLanguageVersion.of(8))
    }
}
