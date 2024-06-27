# Use the official Node.js image as the base image
FROM node:20

# Set the working directory inside the container
WORKDIR /app

# Copy the package.json and package-lock.json files
COPY package*.json ./

# Install the dependencies
RUN npm install

# Copy the entire Angular application source code
COPY . .

# Start the Angular application using ng serve
CMD ["npx", "ng", "serve", "--host", "0.0.0.0", "--port", "4200"]
