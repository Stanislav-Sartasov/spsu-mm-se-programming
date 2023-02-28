plugins {
    kotlin("jvm")
}

val mpjHome = System.getenv("MPJ_HOME") ?: error("Specify `MPJ_HOME` environment variable")
val mpjJar = files("$mpjHome/lib/mpj.jar")

dependencies {
    implementation(mpjJar)
}

java {
    toolchain {
        languageVersion.set(JavaLanguageVersion.of(8))
    }
}
