plugins {
    kotlin("jvm")

    application
}

dependencies {
    implementation(project(":lib"))
}

application {
    mainClass.set("prodcon.app.AppKt")
}

tasks.run.configure {
    standardInput = System.`in`
    standardOutput = System.out
    errorOutput = System.err
}
