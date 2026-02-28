pipeline {
    agent any

    environment {
        BUILD_CONFIGURATION = "Release"
        PATH = "/usr/local/share/dotnet:${env.PATH}"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                echo "Restoring dependencies for configuration: ${env.BUILD_CONFIGURATION}"
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                echo "Building solution with configuration: ${env.BUILD_CONFIGURATION}"
                sh 'dotnet build --configuration "${BUILD_CONFIGURATION}" --no-restore'
            }
        }

        stage('Test') {
            steps {
                echo "Running tests with configuration: ${env.BUILD_CONFIGURATION}"
                sh 'dotnet test --configuration "${BUILD_CONFIGURATION}" --no-build --no-restore --logger "trx;LogFileName=test_results.trx"'
            }
        }
    }

    post {
        always {
            echo "Pipeline finished. Publishing test results."
            junit '**/TestResults/**/*.trx'
        }
        success {
            echo "Pipeline succeeded!"
        }
        failure {
            echo "Pipeline failed. Please check the logs for details."
        }
    }
}