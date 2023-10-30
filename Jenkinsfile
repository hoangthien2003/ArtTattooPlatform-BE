pipeline {

    agent {
        docker { image 'baothien01/art_tattoo_api:latest' }
    }

    
    stages {
        stage('Check path') {
            steps {
                echo $PATH
            }
        }

        stage('Packaging') {

            steps {
                
                sh 'docker build --pull --rm -f Dockerfile -t art_tattoo/api .'
                
            }
        }

        stage('Push to DockerHub') {

            steps {
                withDockerRegistry(credentialsId: 'dockerhub', url: 'https://index.docker.io/v1/') {
                    sh 'docker tag art_tattoo/api baothien01/art_tattoo_api'
                    sh 'docker push baothien01/art_tattoo_api'
                }
            }
        }

        stage('Deploy Spring Boot to DEV') {
            steps {
                echo 'Deploying and cleaning'
                sh 'docker image pull baothien01/art_tattoo_api:latest'
                sh 'docker container stop convocation2023 || echo "this container does not exist" '
                sh 'echo y | docker container prune '
                sh 'docker container run -d --rm --name ArtTattooAPI -p 8181:80 -p 8004:443  baothien01/art_tattoo_api '
            }
        }
        
 
    }
    post {
        // Clean after build
        always {
            cleanWs()
        }
    }
}
