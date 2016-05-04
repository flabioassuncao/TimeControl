angular.module("timeControl").controller("myProjectsController", function ($scope, config, $http, activityAPI, functionsForHours, $timeout, $location, $q, localStorageService) {
  $scope.divAddMember = false;
  $scope.txtNameProj = false;
  $scope.messageAdd = ""; 
  var Ids = {};
  
  $scope.createProject = function (nameProject){
      var project = {
          ProjectName: nameProject
      };
        activityAPI.saveProject(project).success(function (data) {
            localStorageService.set('idProject', { lastIdProject: data.ProjectId});
            var authen = activityAPI.authentication;
            Ids.MemberId = authen.idUser;
            Ids.ProjectId = data.ProjectId;
            activityAPI.saveBelongProject(Ids).success(function () {
                getAllProjects();
            });
            $scope.divAddMember = true;
            $scope.txtNameProj = true;
		});
  }
  
  $scope.AttachUser = function(userID) { 
        Ids.MemberId = userID;
        var authData = localStorageService.get('idProject');
        Ids.ProjectId = authData.lastIdProject;
        activityAPI.saveBelongProject(Ids).success(function (data) {
            $scope.message = "User added to the project!";       
        }).error(function(data, status) {
          $scope.message = "User already exists in the project.";
        });
    }
    
  $scope.addUserToProject = function(memberId, projectId) {
      Ids.ProjectId = projectId;
      Ids.MemberId = memberId;
      activityAPI.saveBelongProject(Ids).success(function (data) {
          updateMembers(projectId);
          $scope.messageAdd = "User added to the project!";
        }).error(function(data, status) {
          $scope.messageAdd = "User already exists in the project.";
        });
  }
    
  var getAllProjects = function(){
      activityAPI.getAllProjects().success(function(data) {
        $scope.users = data;
        $('#loader').hide();
        $('#userList').show();
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            activityAPI.getNameUsers().success(function (data) {
                $scope.members = data;
            });
        }, 2000);
            
      }).error(function(data, status) {
            alert('get data error!');
      });
  }
  
  $scope.removerUserProject = function(memberId, projectId) {
      activityAPI.deleteBelong(memberId, projectId).success(function () {
          updateMembers(projectId);
          $scope.messageAdd = "User removed to the project!";
      });
  }
  
  $scope.showDetail = function (u) {
    if ($scope.active != u.ProjectName) {
        updateMembers(u.ProjectId);
        $scope.active = u.ProjectName;
        $scope.messageAdd = "";
    }
    else
      $scope.active = null;
  }
  
  var updateMembers = function (projectId) {
      activityAPI.getBelongProject(projectId).success(function (data) {
        $scope.BelongToProject = data;
      });
  }
  
  getAllProjects();
  activityAPI.checkAuthentication();
});