pipeline {
    agent any
    
    environment {
        DOCKER_IMAGE = 'scm1-api'
        DOCKER_COMPOSE_PROJECT = 'scm1'
    }
    
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        
        stage('Build') {
            steps {
                sh 'dotnet restore SCM1.sln'
                sh 'dotnet build SCM1.sln -c Release'
            }
        }
        
        stage('Docker Build') {
            steps {
                sh 'docker build -t ${DOCKER_IMAGE}:${BUILD_NUMBER} .'
            }
        }
        
        stage('Deploy to Staging') {
            steps {
                sh 'docker-compose -p ${DOCKER_COMPOSE_PROJECT} up -d --build'
            }
        }
    }
    
    post {
        success {
            echo 'Deployment successful!'
        }
        failure {
            echo 'Deployment failed.'
        }
    }
}
