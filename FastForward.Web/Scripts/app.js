var app = angular.module("ff", [])
    .directive("module", ["cssInjector", function (cssInjector) {
        return {
            scope: { id: "@", module: "@" },
            replace: true,
            restrict: "A",
            templateUrl: function (elm, attrs) { return baseUrl + attrs.module + "/main.html" },
            compile: function () {
                return {
                    pre: function ($scope) {
                        cssInjector.get($scope.module, baseUrl + $scope.module + "/main.css");
                    }
                }
            },
            controller: "default"
        };
    }])
    .controller("default", ["$scope", "$http", function ($scope, $http) {
        var dataUrl = (mode === "debug") ? baseUrl + $scope.module + "/main.json" : serviceUrl + $scope.id;
        $http.get(dataUrl, { cache: true })
            .success(function (data) {
                $scope.model = data;
            });
    }])
    .factory("cssInjector", function ($q, $http) {
        var injected = {},
            header = angular.element(document.querySelector('head'));
        injected.get = function (id, url) {
            var cssId = "css-" + id;
            $http.get(url, { cache: true })
                .success(function () {
                    if (!header.find("link#" + cssId).length) {
                        header.append("<link id=\"" + cssId + "\" href=\"" + url + "\" rel=\"stylesheet\">");
                    };
                });
        };
        return injected;
    });
angular.bootstrap(document, ["ff"]);


