var mode = "release",
    baseUrl = "/scripts/app/modules/",
    serviceUrl = "/api/dynamic/model/";

var app = angular.module("ff", [])
    .directive("module", ["styles", function (styles) {
        return {
            scope: { id: "@", module: "@" },
            replace: true,
            restrict: "A",
            templateUrl: function (elm, attrs) { return baseUrl + attrs.module + "/main.html" },
            compile: function () {
                return {
                    pre: function ($scope) {
                        styles.get($scope.module, baseUrl + $scope.module + "/main.css");
                    }
                }
            },
            controller: "default"
        };
    }])
    .controller("default", ["$scope", "$http", "controllers", function ($scope, $http, controllers) {
        var dataUrl = (mode === "debug") ? baseUrl + $scope.module + "/main.json" : serviceUrl + $scope.id;
        $http.get(dataUrl, { cache: true })
            .success(function (data) {
                $scope.model = data;
                controllers.get($scope);
            });
    }])
    .factory("controllers", function ($q, $http, $injector) {
        var factory = {};
        factory["get"] = function ($scope) {
            var url = baseUrl + $scope.module + "/main.js";
            $http.get(url, { cache: true }).success(function (f) {
                var fn = new Function("$scope", f);
                $injector.invoke(fn($scope));
            });
        };
        return factory;
    })
    .factory("styles", function ($q, $http) {
        var factory = {},
            header = angular.element(document.querySelector('head'));
        factory["get"] = function (id, url) {
            var cssId = "css-" + id;
            $http.get(url, { cache: true })
                .success(function () {
                    if (!header.find("link#" + cssId).length) {
                        header.append("<link id=\"" + cssId + "\" href=\"" + url + "\" rel=\"stylesheet\">");
                    };
                });
        };
        return factory;
    })
;
angular.bootstrap(document, ["ff"]);


