angular.module('MyApp')
.controller('RegistationController', function ($scope, RegistrationService) {
    //Default Variable
    $scope.submitText = "Save";
    $scope.submitted = false;
    $scope.message = '';
    $scope.isFormValid = false;
    $scope.User = {
        UserName: '',
        Password: '',
        FullName: '',
        EmailID: '',
        Gender: ''
    };
    //validates form on client side
    $scope.$watch('f1.$valid', function (newValue) {
        $scope.isFormValid = newValue;
    });
    //Save Data
    $scope.SaveData = function (data) {
        if ($scope.submitText == 'Save') {
            $scope.submitted = true;
            $scope.message = '';

            if ($scope.isFormValid) {
                $scope.submitText = 'Please Wait...';
                $scope.User = data;
                RegistrationService.SaveFormData($scope.User).then(function (d) {
                    alert(d);
                    if (d == 'Success') {
                        //have to clear form here
                        ClearForm();
                    }
                    $scope.submitText = "Save";
                });
            }
            else {
                $scope.message = '';
            }
        }
    }
    //Clear Form 
    function ClearForm() {
        $scope.User = {};
        $scope.f1.$setPristine(); //here f1 is form name
        $scope.submitted = false;
    }
})
.factory('RegistrationService', function ($http, $q) {

    var fac = {};
    fac.SaveFormData = function (data) {
        var defer = $q.defer();
        $http({
            url: '/Register',
            method: 'POST',
            data: JSON.stringify(data),
            headers: { 'content-type': 'application/json' }
        }).success(function (d) {
            // Success callback
            defer.resolve(d);
        }).error(function (e) {
            //Failed Callback
            alert('Error!');
            defer.reject(e);
        });
        return defer.promise;
    }
    return fac;
});

