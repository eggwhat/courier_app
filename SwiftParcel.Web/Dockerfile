# Use the official Node.js 18 image as a base image
FROM node:18

# Set the working directory in the container
WORKDIR /usr/src/app

# Navigate to the frontend directory and copy package.json files
COPY SwiftParcel.Web/frontend/package*.json ./

# Install the project dependencies
RUN npm install

# Copy the rest of the frontend directory into the container
COPY SwiftParcel.Web/frontend/ .

# Make port 3000 available to the world outside this container
EXPOSE 3000

# Define the command to run your app using CMD which defines your runtime
CMD ["npm", "start"]
