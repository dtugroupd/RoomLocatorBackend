pipeline {
  agent any
  stages {
    stage('Docker') {
      steps {
        sh '''cd RoomLocator/
docker build -t dtugroupd/room-locator:latest .
docker login --username hudhud3101 --password c6c68806-cb13-448e-a306-7792923cdfa6
docker push dtugroupd/room-locator:latest
ssh s165241@se2-webapp04.compute.dtu.dk -i ~/.ssh/deploy /opt/update-api.sh'''
      }
    }
  }
}