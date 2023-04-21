import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
	kotlin("jvm") version "1.6.20"
	application
	jacoco
}

group = "me.chezychez"
version = "1.0-SNAPSHOT"

jacoco {
	toolVersion = "0.8.7"
}

repositories {
	mavenCentral()
}

dependencies {
	implementation("org.junit.jupiter:junit-jupiter:5.8.1")
	testImplementation(kotlin("test"))
	testImplementation("org.junit.jupiter:junit-jupiter-api:5.8.2")
	testRuntimeOnly("org.junit.jupiter:junit-jupiter-engine:5.8.2")
	testImplementation("org.junit.jupiter:junit-jupiter-params:5.8.2")
	implementation(kotlin("reflect"))
	implementation(project(":lib"))
	implementation(project(":bots"))
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
}

tasks.withType<KotlinCompile> {
	kotlinOptions.jvmTarget = "1.8"
}

application {
	mainClass.set("MainKt")
}