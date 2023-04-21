import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
	kotlin("jvm") version "1.6.20"
	application
	id("org.openjfx.javafxplugin") version "0.0.12"
}

repositories {
	mavenCentral()
}

dependencies {
	implementation("no.tornado:tornadofx:1.7.20")
	implementation(project(":WeatherServices"))

	testImplementation(kotlin("test"))
	testImplementation("org.junit.jupiter:junit-jupiter:5.8.2")
}


tasks.withType<KotlinCompile> {
	kotlinOptions.jvmTarget = "11"
}

tasks.test {
	useJUnitPlatform()
}

application {
	mainClass.set("MainKt")
}

javafx {
	version = "11.0.2"
	modules = listOf("javafx.controls")
}