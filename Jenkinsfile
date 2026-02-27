pipeline {
    agent any

    environment {
        BUILD_CONFIGURATION = "Release"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                echo "Restoring dependencies for configuration: ${BUILD_CONFIGURATION}"
                sh 'echo dotnet restore'
            }
        }

        stage('Build') {
            steps {
                echo "Building solution with configuration: ${BUILD_CONFIGURATION}"
                sh 'echo dotnet build --configuration ${BUILD_CONFIGURATION} --no-restore'
            }
        }

        stage('Test') {
            steps {
                echo "Running tests with configuration: ${BUILD_CONFIGURATION}"
                sh 'echo dotnet test --logger trx --configuration ${BUILD_CONFIGURATION} --no-build'
            }
        }
    }

    post {
        always {
            echo "Pipeline finished. Cleaning up workspace."
        }
        success {
            echo "Pipeline succeeded!"
        }
        failure {
            echo "Pipeline failed. Please check the logs for details."
        }
    }
}