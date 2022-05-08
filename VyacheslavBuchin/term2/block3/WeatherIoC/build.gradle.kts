import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
	kotlin("jvm") version "1.6.20"
	application
	jacoco
}

group = "me.chezychez"
version = "1.0-SNAPSHOT"

repositories {
	mavenCentral()
}

dependencies {
	testImplementation(kotlin("test"))
	testImplementation("org.mockito.kotlin:mockito-kotlin:4.0.0")
	testImplementation("org.junit.jupiter:junit-jupiter-api:5.8.2")
	testRuntimeOnly("org.junit.jupiter:junit-jupiter-engine:5.8.2")
	testImplementation("org.junit.jupiter:junit-jupiter-params:5.8.2")
	implementation(group="com.googlecode.json-simple", name="json-simple", version="1.1.1")
	implementation("org.kodein.di:kodein-di:7.11.0")
}

jacoco {
	toolVersion = "0.8.7"
}

tasks.test {
	useJUnitPlatform()
	finalizedBy(tasks.jacocoTestReport)
}

tasks.jacocoTestReport {
	dependsOn(tasks.test)
	reports {
		xml.required.set(false)
		csv.required.set(false)
		html.outputLocation.set(layout.buildDirectory.dir("jacocoHtml"))
	}
	classDirectories.setFrom(
		files(classDirectories.files.map {
			fileTree(it) {
				exclude(
					"**/service/weather/tomorrow_io/provider",
					"**/service/weather/open_weather_map/provider",
					"**/kodein",
					"**/*Main*"
				)
			}
		})
	)
}

tasks.withType<KotlinCompile> {
	kotlinOptions.jvmTarget = "16"
}

application {
	mainClass.set("MainKt")
}