pipeline {
    agent any

    environment {
        NUGET_API_KEY = credentials("NUGET_API_KEY")
        PACKAGE_VERSION = "1.0.0.${env.BUILD_NUMBER}"
        PACKAGE_ID = "TimeCare.WorkSchedule"
        PACKAGE_PATH = "bin\\release\\${env.PACKAGE_ID}.${env.PACKAGE_VERSION}.nupkg"
        PUBLISH_PACKAGE = "false"
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
        
        stage("Publish to NuGet package") {
            when {
                environment name: "PUBLISH_PACKAGE", value: "true"
            }
            steps {
                dir('TimeCare.WorkSchedule') {
                    bat "dotnet pack --no-build -c %CONFIGURATION% . /property:PackageVersion=%PACKAGE_VERSION%"
                    bat "dotnet nuget push %PACKAGE_PATH% --source https://www.nuget.org/api/v2/package --api-key %NUGET_API_KEY%"
                }
            }
        }
    }
    
    post {
        always {
            step([$class: "XUnitBuilder", thresholds: [[$class: "FailedThreshold", failureThreshold: "0"]], 
                tools: [[$class: "CustomType", deleteOutputFiles: false, pattern: "**/*.xml", customXSL: "tools/xunitdotnet-2.0-to-junit-2.xsl"]]])

            cleanWs()
        }
    }
}