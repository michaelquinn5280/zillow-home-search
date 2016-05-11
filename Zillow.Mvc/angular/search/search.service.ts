(() => {
    'use strict';

    var searchService = angular.module('searchService', ['ngResource']);

    searchService.factory('HomeSearch', ['$resource',
        $resource => $resource('api/search/:criteria', { criteria: '@criteria' }, {
            query: { method: 'GET', params: {}, isArray: false }
        })]);
})();