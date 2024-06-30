# Start with a base image containing Java runtime
FROM amazoncorretto:21-alpine-jdk AS build

# Create a directory for the app
WORKDIR /app

# Copy the Gradle wrapper and build configuration files first
COPY gradlew .
COPY gradle gradle
COPY build.gradle.kts .
COPY settings.gradle.kts .

# Make the Gradle wrapper executable
RUN chmod +x gradlew

# Copy the rest of the application source code
COPY src src

# Build the project avoiding tests
RUN ./gradlew clean build -x test

# Create a new stage for the runtime
FROM amazoncorretto:21-alpine-jdk

# Create a directory for the app
WORKDIR /app

# Copy the built JAR file from the build stage
COPY --from=build /app/build/libs/*.jar /app/demo.jar

# Expose port 8080
EXPOSE 8080

# Run the JAR file
CMD ["java", "-jar", "/app/demo.jar"]