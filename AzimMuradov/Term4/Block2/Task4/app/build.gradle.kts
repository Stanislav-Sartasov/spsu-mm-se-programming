plugins {
    kotlin("jvm")

    application
}

dependencies {
    implementation(project(":lib"))
}

application {
    mainClass.set("pools.app.AppKt")
}
