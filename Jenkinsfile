pipeline {
    agent any

    environment {
        NUGET_API_KEY = credentials("NUGET_API_KEY")
        PACKAGE_VERSION = "1.0.0.${env.BUILD_NUMBER}"
        PACKAGE_ID = "TimeCare.WorkSchedule"
        PACKAGE_PATH = "bin\\release\\${env.PACKAGE_ID}.${env.PACKAGE_VERSION}.nupkg"
        CONFIGURATION = "Release"
    }
    
    stages {
        stage("Checkout") {
            steps {
                checkout scm
            }
        }
        
        stage("Build") {
            steps {
                bat "dotnet restore"
                bat "dotnet build -c %CONFIGURATION%"
            }
        }
        
        stage("Test") {
            steps {
                parallel("Unit Tests": {
                    dir("TimeCare.WorkSchedule.UnitTests") {
                        echo "Run unit tests"
                        bat "dotnet xunit -configuration %CONFIGURATION% -nobuild -xml unittests.xml"
                    }
                }, "Integration tests": {
                    dir("TimeCare.WorkSchedule.IntegrationTests") {
                        echo "Run integration tests"
                        bat "dotnet xunit -configuration %CONFIGURATION% -nobuild -xml integrationtests.xml"
                    }
                })
            }
        }
        
        stage("Merge tests") {
            steps {
                step([$class: "XUnitBuilder", thresholds: [[$class: "FailedThreshold", failureThreshold: "0"]], 
                            tools: [[$class: "CustomType", deleteOutputFiles: false, pattern: "**/*.xml", customXSL: "tools/xunitdotnet-2.0-to-junit-2.xsl"]]])
            }
        }
    }
    
    post {
        always {
            cleanWs()
        }
    }
}