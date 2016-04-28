angular.module("timeControl").controller("myProjectsController", function ($scope, config, $http, activityAPI, functionsForHours, $timeout, $location, $q, localStorageService) {
  
  $scope.divAddMember = false;
  $scope.txtNameProj = false;
    
  $scope.getOptions2 = function(){
        
        activityAPI.getNameUsers().success(function (data) {
                $scope.members = data;
            }).error(function (data, status) {
            });
  }
  
  $scope.createProject = function (nameProject){
      var project = {};
        project.ProjectName = nameProject;
        project.AdministratorId = "fe384930-b71a-4013-9694-1f48bc436fb0";
        activityAPI.saveProject(project).success(function (data) {
            console.log(data.ProjectId);
            localStorageService.set('idProject', { lastIdProject: data.ProjectId});
            $scope.divAddMember = true;
            $scope.txtNameProj = true;
		});
  }
  
  $scope.AttachUser = function(userID) {
    //   console.log(userID);
        var Ids = {};
        Ids.MemberId = userID;
        var authData = localStorageService.get('idProject');
        Ids.ProjectId = authData.lastIdProject;
        activityAPI.saveBelongProject(Ids).success(function (data) {
            $scope.message = "user adicionado ao proj"            
        });
    }
    
  $scope.addUserToProject = function(memberId, projectId) {
      var belong = {};
      belong.ProjectId = projectId;
      belong.MemberId = memberId;
      activityAPI.saveBelongProject(belong).success(function (data) {
            console.log('ok');            
        });
  }  
  
  $scope.addUser = function () {
        if($scope.divUsers)
            $scope.divUsers = false;
        else   
            $scope.divUsers = true;
    }
  
  $http.get(config.baseUrl + "/Project").success(function(data) {
    $scope.users = data;
    $('#loader').hide();
    $('#userList').show();
  }).error(function(data, status) {
    alert('get data error!');
  });
  
  $scope.showDetail = function (u) {
    if ($scope.active != u.ProjectName) {
      $scope.active = u.ProjectName;
    }
    else {
      $scope.active = null;
    }
  };
  
  $scope.doPost = function() {
  
    $http.get('http://api.randomuser.me/0.4/').success(function(data) {
      
      var newUser = data.results[0];
      newUser.user.text = $('#inputText').val();
      newUser.date = new Date();
      $scope.users.push(newUser);
   
    }).error(function(data, status) {
      
      alert('get data error!');
      
    });
    
  }
    
});