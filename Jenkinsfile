pipeline {
    agent any

    environment {
        // Set environment variables if necessary
        DOCKER_IMAGE_NAME = 'your-docker-image-name'
        DOCKER_REGISTRY = 'your-docker-registry'
    }

    stages {
        stage('Checkout') {
            steps {
                // Checkout the code from Git repository
                git credentialsId: 'github-credentials', url: 'https://github.com/mer486/SCM1.git'
            }
        }

        stage('Build') {
            steps {
                script {
                    // Run the build steps here
                    echo 'Building the project...'

                    // Example: Running a batch command on Windows
                    bat 'echo Building project on Windows'
                }
            }
        }

        stage('Docker Build') {
            steps {
                script {
                    // Build Docker image (make sure Docker is installed and configured on Windows)
                    echo 'Building Docker image...'
                    bat 'docker build -t %DOCKER_IMAGE_NAME% .'
                }
            }
        }

        stage('Deploy to Staging') {
            steps {
                script {
                    // Deploy to staging (you can add Windows-specific deployment commands)
                    echo 'Deploying to staging...'
                    bat 'docker run -d %DOCKER_IMAGE_NAME%'
                }
            }
        }
    }

    post {
        success {
            echo 'Pipeline executed successfully!'
        }
        failure {
            echo 'Pipeline failed!'
        }
    }
}
