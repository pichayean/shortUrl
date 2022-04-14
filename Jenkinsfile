def code  = '';
def notify(tag, message, stickerId) {
    def token = "vev2IewXbmQjYDpzKrFHmDBAEmLseBZqDR5H1kATykA";
    def buildNo = env.BUILD_NUMBER;

    def url = "https://notify-api.line.me/api/notify";
    def lineMessage = "todo-${message}:${tag}:buildNo-[${buildNo}] \r\n";
	sh "curl ${url} -H 'Authorization: Bearer ${token}' -F 'message=${lineMessage}' -F 'stickerId=${stickerId}' -F 'stickerPackageId=1'";
}
def appName = "jenkins-k8s-deployment";
def getDockerTag(){
    def tag  = sh script: 'git rev-parse HEAD', returnStdout: true
    return tag
}

def getDeploymentReady(){
    def isRunning  = sh script: 'kubectl get pods --field-selector=status.phase=Running  | grep -c jenkins-k8s-deployment', returnStdout: true
    return isRunning
}

pipeline {
    agent any
    environment {
        NEW_VERSION = '1.0.0'
        CI = 'true'
        DOCKER_TAG = getDockerTag()
        DOCKER_OLD_TAG = getOldDockerTag()
        statz_build = '';
    	count = 0
    }
    stages {
	stage('Check pod ready') {
            steps {
		    
	 	def count = 5
def fact = 1
do {
    fact *= count--
} while(count > 1)
		//sh "while [[ $(kubectl get pods --field-selector=status.phase=Running  | grep -c jenkins-k8s-deployment) != 0 ]]; do sleep 5; kubectl get deploy jenkins-k8s-deployment; done;"
		sh "echo 'success'"
		//sh "for value in 1 2 3 4 5; do sleep 15; echo 123; done"
            }
        }
        // stage('Check Staging Ready') {
        //     steps {
        //         script {
        //             try {
        //                 sleep 40 
        //                 code = sh(returnStdout: true, script: "curl -o /dev/null -s -w '%{http_code}' http://194.233.73.42:5009/Healthz").trim()
        //                 echo "HTTP response status code: ${code}"
        //             } catch (Exception e){
        //                 sh "docker image rm -f serverinfo:${DOCKER_TAG}";
        //            	    notify(DOCKER_TAG, 'Something wrong (stage[Check Staging Ready]) 🤳🤳', '9')
        //             }
        //         }
        //     }
        // }
        // stage('RemoveOldVersionImages') {
        //     steps {
        //         script {
        //             if (code == '200') {
        //                 echo '---remove old version images---'
        //                 sh "docker image rm -f serverinfo:${DOCKER_OLD_TAG}";
        //                 notify(DOCKER_TAG, 'Deploy new version Success 😜💖', '113')
        //             } 
        //         }
        //     }
        // }
    }
}
