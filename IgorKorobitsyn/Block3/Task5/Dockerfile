FROM openjdk:11-jre-slim

COPY build.gradle.kts settings.gradle.kts ./
COPY gradle ./gradle
COPY gradlew gradlew.bat ./
COPY src ./src

RUN chmod +x ./gradlew
RUN ./gradlew shadowJar
RUN mv build/libs/Task5-1.0-SNAPSHOT-all.jar app.jar

#WORKDIR /usr/src/main/kotlin
#
#COPY build/libs/*.jar app.jar

EXPOSE 8080

CMD ["java", "-jar", "app.jar"]
