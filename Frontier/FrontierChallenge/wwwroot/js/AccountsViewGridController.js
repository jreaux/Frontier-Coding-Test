(function () {
    'use strict';

    angular
        .module('accountsViewGridApp', [])
        .filter('telephone', function () {
            return function (phoneNumber /*input*/) {
                var regexObj = /^(?:\+?1[-. ]?)?(?:\(?([0-9]{3})\)?[-. ]?)?([0-9]{3})[-. ]?([0-9]{4})$/;
                if (regexObj.test(phoneNumber)) {
                    var phoneNumberParts = phoneNumber.match(regexObj);
                    var formattedPhoneNumber = "";
                    if (phoneNumberParts[1]) {
                        formattedPhoneNumber += "+1 (" + phoneNumberParts[1] + ") ";
                    }
                    formattedPhoneNumber += phoneNumberParts[2] + "-" + phoneNumberParts[3];
                    return formattedPhoneNumber;
                } else {
                    //invalid phone number
                    return phoneNumber;
                }
            };
        })
        .controller('accountsViewGridController', controller);

    controller.$inject = ['$scope', '$http'];

    function controller($scope, $http) {
        getAccountsList($scope, $http);
    }

    function getAccountsList($scope, $http) {
        // This url normally would not be hard-coded like this here but 
        // this is just for demonstration purposes.
        var getUrl = "http://localhost:7071/api/GetAccountsList";
        $http.get(getUrl).then(
            function (response) {
                console.log(response.data);
                $scope.accountsList = response.data;
            },
            function errorCallback(error) {
                console.log(error);
                $scope.accountsList = [];
            });
    }
})();
