plugins {
    kotlin("jvm")

    application
}

dependencies {
    implementation(project(":lib"))
}

application {
    mainClass.set("channels.app.AppKt")
}
