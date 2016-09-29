angular.module('myApp').controller('UserLoginController', function ($scope, LoginService) {

    //initilize user data object
    $scope.LoginData = {
        Email: '',
        Password: ''
    }
    $scope.msg = "";
    $scope.Submited = false;
    $scope.IsLoggedIn = false;
    $scope.IsFormValid = false;

    //Check whether the form is valid or not using $watch
    $scope.$watch("myForm.$valid", function (TrueOrFalse) {
        $scope.IsFormValid = TrueOrFalse;   //returns true if form valid
    });

    $scope.LoginForm = function () {
        $scope.Submited = true;
        if ($scope.IsFormValid) {
            LoginService.getUserDetails($scope.UserModel).then(function (d) {
                debugger;
                if (d.data.Email != null) {
                    debugger;
                    $scope.IsLoggedIn = true;
                    $scope.msg = "You successfully Loggedin Mr/Ms " + d.data.FullName;
                }
                else {
                    alert("Invalid credentials buddy! try again");
                }
            });
        }
    }
})
    .factory("LoginService", function ($http) {
        //initilize factory object.
        var fact = {};
        fact.getUserDetails = function (d) {
            debugger;
            return $http({
                url: '/Home/getLoginData',
                method: 'POST',
                data: JSON.stringify(d),
                headers: { 'content-type': 'application/json' }
            });
        };
        return fact;
    });