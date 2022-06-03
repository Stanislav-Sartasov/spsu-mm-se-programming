import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
	kotlin("jvm") version "1.6.21"
	application
	jacoco
}

group = "me.chezychez"

repositories {
	mavenCentral()
}

dependencies {
	testImplementation(kotlin("test"))
	testImplementation(kotlin("test"))
	testImplementation("org.mockito.kotlin:mockito-kotlin:4.0.0")
	testImplementation("org.junit.jupiter:junit-jupiter-api:5.8.2")
	testRuntimeOnly("org.junit.jupiter:junit-jupiter-engine:5.8.2")
	testImplementation("org.junit.jupiter:junit-jupiter-params:5.8.2")
	implementation(group="com.googlecode.json-simple", name="json-simple", version="1.1.1")
}

jacoco {
	toolVersion = "0.8.7"
}

tasks.test {
	useJUnitPlatform()
	finalizedBy(tasks.jacocoTestReport)
}

tasks.withType<KotlinCompile> {
	kotlinOptions.jvmTarget = "16"
}