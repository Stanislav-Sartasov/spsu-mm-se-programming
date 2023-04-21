import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
	kotlin("jvm") version "1.6.10"
	id("org.jetbrains.compose") version "1.1.1"
}

repositories {
	mavenCentral()
	maven("https://maven.pkg.jetbrains.space/public/p/compose/dev")
	google()
}

dependencies {
	implementation(compose.desktop.currentOs)
	implementation(project(":WeatherServices"))
	testImplementation(kotlin("test"))
	testImplementation("org.junit.jupiter:junit-jupiter:5.8.2")
}


tasks.withType<KotlinCompile> {
	kotlinOptions.jvmTarget = "16"
	kotlinOptions.freeCompilerArgs += "-opt-in=kotlin.RequiresOptIn"
}

tasks.test {
	useJUnitPlatform()
}

compose.desktop {
    application {
        mainClass = "MainKt"
    }
}