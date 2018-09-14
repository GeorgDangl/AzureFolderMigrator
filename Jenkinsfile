pipeline {
    agent {
        node {
            label 'master'
            customWorkspace 'workspace/AzureFolderMigrator'
        }
    }
    environment {
        KeyVaultBaseUrl = credentials('AzureCiKeyVaultBaseUrl')
        KeyVaultClientId = credentials('AzureCiKeyVaultClientId')
        KeyVaultClientSecret = credentials('AzureCiKeyVaultClientSecret')
    }
    stages {
        stage ('Deploy') {
            steps {
                powershell './build.ps1 PublishGitHubRelease'
            }
        }
    }
    post {
        always {
            step([$class: 'Mailer',
                notifyEveryUnstableBuild: true,
                recipients: "georg@dangl.me",
                sendToIndividuals: true])
        }
    }
}
