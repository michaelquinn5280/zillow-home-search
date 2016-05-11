(function () {
    'use strict';
    var searchService = angular.module('searchService', ['ngResource']);
    searchService.factory('HomeSearch', ['$resource',
        function ($resource) { return $resource('api/search/:criteria', { criteria: '@criteria' }, {
            query: { method: 'GET', params: {}, isArray: false }
        }); }]);
})();
//# sourceMappingURL=search.service.js.map