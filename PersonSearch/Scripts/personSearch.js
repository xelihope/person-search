var app = angular.module("app", []);

app.controller("peopleCtrl", function ($scope, $http, $timeout) {
    $scope.people = [];
    $scope.pageSize = 9;
    $scope.currentPage = 0;
    $scope.nameFilter = "";
    $scope.totalCount = 0;
    $scope.showAdd = false;

    $scope.openDialog = function () {
        $("#addDialog").dialog("open");
    };

    $scope.getFirstPage = function () {
        $scope.currentPage = 0;
        $scope.totalCount = 0;
        $scope.people = [];
        $scope.getPage(0);
    };

    $scope.getPage = function (pageDiff) {
        let postData = { pageSize: $scope.pageSize, page: $scope.currentPage + pageDiff, nameFilter: $scope.nameFilter };
        $(".loadingIcon").show();

        // Each page has been slowed down by 2 seconds on purpose par the slowness request
        // CHANGE HERE if you want the slowness to go away
        $timeout(function () {
            $.when($http.post("/api/people", postData), $http.post("/api/people/count", '"' + $scope.nameFilter + '"'))
                .then(function (res1, res2) {
                    $scope.currentPage = $scope.currentPage + pageDiff;
                    $scope.people = res1.data;
                    $scope.totalCount = res2.data;
                    $scope.$apply();
                    $(".loadingIcon").hide();
                });
        }, 2000);
    };

    $scope.getStartCount = function () {
        return Math.min($scope.currentPage * $scope.pageSize + 1, $scope.totalCount);
    };

    $scope.getEndCount = function () {
        return Math.min($scope.currentPage * $scope.pageSize + $scope.pageSize, $scope.totalCount);
    };

    $scope.requiresPaging = function () {
        return $scope.totalCount > $scope.pageSize;
    };

    $scope.getHobbies = function (person) {
        return person.hobbies.join(", ");
    };

    $scope.showHobbies = function (person) {
        return person.hobbies !== null && person.hobbies.length > 0;
    };

    $scope.getPage(0);
});