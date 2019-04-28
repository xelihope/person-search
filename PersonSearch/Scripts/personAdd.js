angular.module("app")
    .controller("personAddCtrl", function ($scope, $http, $timeout) {
        $scope.formData = {};
        $scope.hobbies = [];

        $scope.pictureClick = function ($event) {
            $($event.currentTarget).next().click();
        };

        $scope.pictureChange = function () {
            var reader = new FileReader();
            reader.onload = function () {
                $("#pictureOutput").attr("src", reader.result);
            };
            reader.readAsDataURL(event.target.files[0]);
        };

        $scope.cancel = function () {
            $("#addDialog").dialog("close");
        };

        $scope.save = function () {
            $http.put("/api/person", $scope.formData)
                .then(function (res1) {
                    let data = new FormData();
                    data.append("pictureFile", $("#pictureFile")[0].files[0]);
                    $http({
                        method: "POST",
                        url: "/api/person/picture/" + res1.data,
                        data: data,
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    }).then(function (res2) {
                        window.location.href = "/";
                    }, function (res2) {
                            alert(res2.data.exceptionMessage);
                    });
                }, function (res1) {
                    alert(res1.data.exceptionMessage);
                });
        };

        $scope.getHobbies = function () {
            $http.get("/api/person/hobbies")
                .then(function (res) {
                    $scope.hobbies = res.data;
                });
        };

        $(".useDatePicker").datepicker();
        $("#addDialog").dialog({
            autoOpen: false,
            modal: true,
            resizable: false,
            title: "Add Person",
            height: 460,
            width: 700
        });
        $("#pictureFile").change($scope.pictureChange);
        $("select").selectmenu();
    });