pipeline {
    agent any

    environment {
        PATH = "/usr/local/share/dotnet:${env.PATH}"
    }

    parameters {
        choice(
            name: 'BUILD_CONFIGURATION',
            choices: ['Debug', 'Release'],
            description: 'Select the build configuration'
        )
        booleanParam(
            name: 'RUN_TESTS',
            defaultValue: true,
            description: 'Whether to run tests after building'
        )
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                echo "Restoring dependencies for configuration: ${params.BUILD_CONFIGURATION}"
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                echo "Building solution with configuration: ${params.BUILD_CONFIGURATION}"
                sh "dotnet build --configuration ${BUILD_CONFIGURATION} --no-restore"
            }
        }

        stage('Test') {
            when {
                expression {
                    params.RUN_TESTS == true
                }
            }
            steps {
                echo "Running tests with configuration: ${params.BUILD_CONFIGURATION}"
                sh "dotnet test --configuration ${BUILD_CONFIGURATION} --no-build --no-restore --logger \"junit;LogFilePath=TestResults/junit.xml\""
            }
        }
    }

    post {
        always {
            echo "Pipeline finished. Publishing test results."
            junit '**/TestResults/**/*.xml'
        }
        success {
            echo "Pipeline succeeded!"
        }
        failure {
            echo "Pipeline failed. Please check the logs for details."
        }
    }
}