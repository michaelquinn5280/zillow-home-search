(() => {
    'use strict';

    angular
        .module('zillowApp')
        .controller('searchController', searchController);

    searchController.$inject = ['$scope', 'HomeSearch'];

    function searchController($scope, HomeSearch) {
        //might as well default to disneyland...
        $scope.latitude = 33.8120962;
        $scope.longitude = -117.9211629;
        $scope.map = {
            options: () => ({
                streetViewControl: false,
                scrollwheel: false
            })
        };

        $scope.searchHomes = () => {
            HomeSearch.query({ criteria: $scope.searchedValue },
            (data) => {
                $scope.responseMessage = data.text;
                $scope.results = "";
                if (data.results[0] !== 'undefined') {
                    $scope.results = data.results[0];
                    $scope.latitude = data.results[0].address.latitude;
                    $scope.longitude = data.results[0].address.longitude;
                }
            },
            (error) => {
                $scope.results = "";
                if (error.data.length > 0) {
                    $scope.responseMessage = error.data;
                } else {
                    $scope.responseMessage = error.statusText;
                }
            });
        };
    }
})();