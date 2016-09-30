angular.module('MyApp').controller('LoginController', function ($scope, LoginService) {

    //initilize user data object
    $scope.LoginData = {
        Username: '',
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
            LoginService.getUserDetails($scope.User).then(function (d) {
                debugger;
                if (d.data.Username != null) {
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
                url: '/Login/getLoginData',
                method: 'POST',
                data: JSON.stringify(d),
                headers: { 'content-type': 'application/json' }
            });
        };
        return fact;
    });