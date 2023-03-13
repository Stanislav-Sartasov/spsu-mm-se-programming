plugins {
    kotlin("jvm")

    application
}

dependencies {
    implementation(project(":lib"))
}

application {
    mainClass.set("fibers.app.AppKt")
}
