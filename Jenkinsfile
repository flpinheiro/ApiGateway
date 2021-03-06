def getVersion(){
    def matchar = readFile('ApiGateway/ApiGateway.csproj') =~ '<Version>(.+)</Version>'
    matchar ? matchar[0][1] : null
}

pipeline{
    agent any
    environment{
        VERSION = getVersion()
    }
    stages{
        stage('Clean'){
            steps{
                sh 'dotnet clean ApiGateway.sln -c Release'
            }
            post{
                success{
                    echo "======== dotnet clean successfully ========"
                }
                failure{
                    echo "======== dotnet clean failed ========"
                }
            }
        }

        stage('Restore'){
            steps{
                sh 'dotnet restore ApiGateway.sln'
            }
            post{
                success{
                    echo "======== dotnet restore successfully ========"
                }
                failure{
                    echo "======== dotnet restore failed ========"
                }
            }            
        }

        stage('Test'){
            steps{
                sh 'dotnet test ApiGateway.sln --no-restore --verbosity normal'
            }
            post{
                success{
                    echo "======== dotnet test successfully ========"
                }
                failure{
                    echo "======== dotnet test failed ========"
                }
            }            
        }

        stage("Docker Build"){
            steps{
                sh 'docker build -f ./ApiGateway/Dockerfile -t compuletra/compuletra.api.gateway:$VERSION-$BUILD_NUMBER .'
                sh 'docker tag compuletra/compuletra.api.gateway:$VERSION-$BUILD_NUMBER compuletra/compuletra.api.gateway:latest'
            }
            post{
                success{
                    echo "======== Docker build successfully ========"
                }
                failure{
                    echo "======== docker Build failed ========"
                }
            } 
        }

        stage("Docker Push"){
            steps{
                sh 'docker login -u jenkins -p jenkins 10.0.18.30:8082/docker-hosted'
                sh 'docker push compuletra/compuletra.api.gateway:$VERSION-$BUILD_NUMBER'
                sh 'docker push compuletra/compuletra.api.gateway:latest'
            }
            post{
                success{
                    echo "======== docker Push successfully ========"
                }
                failure{
                    echo "======== docker Push failed ========"
                }
            }             
        }

        stage("Docker Compose"){
            steps{
                    sh 'docker-compose up -d'
            }
            post{
                success{
                    echo "======== docker Compose successfully ========"
                }
                failure{
                    echo "======== docker Compose failed ========"
                }
            }              
        }    
    }
    post{
        success{
            echo "======== pipeline successfully ========"
        }
        failure{
            echo "======== pipeline failed ========"
        }
    }
}
