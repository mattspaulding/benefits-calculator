'use strict';

angular.module('App.employeesServices', []).factory('Employee', function ($resource, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    return $resource(ngAuthSettings.apiServiceBaseUri + 'api/employees/:id', { id: '@employeeId' }, {
        update: {
            method: 'PUT'
        }
    });
});
