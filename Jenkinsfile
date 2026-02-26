pipeline {
    agent any

    stages {
        stage('Build Info') {
            steps {
                echo "Running on ${env.NODE_NAME}"
                echo "Workspace is ${env.WORKSPACE}"
            }
        }

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Simulate Build') {
            steps {
                sh 'echo Building project...'
                sh 'sleep 5'
                sh 'echo Build complete'
            }
        }
    }
}
