// notesView.js
(function (angular) {
    var theModule = angular.module("notesView", ["ui.bootstrap"]);

    theModule.controller("notesViewController", ["$window", "$http",
        function ($window, $http) {
            var vm = this;
            vm.save = save;

            function createBlankNote() {
                return {
                    note: "",
                    color: "yellow"
                };
            };

            vm.notes = [];
            vm.newNote = createBlankNote();

            //Get the category name
            var urlParts = $window.location.pathname.split("/");
            var categoryName = urlParts[urlParts.length - 1];
            var notesUrl = "/api/note/" + categoryName;
            $http.get(notesUrl).then(function (result) {
                vm.notes = result.data;
            }, function (err) {
                // Error
                alert(err);
           });

            function save() {
                $http.post(notesUrl, vm.newNote).then(function (result) {
                    //success
                    vm.notes.push(result.data);
                    vm.newNote = createBlankNote();
                }, function (err) {
                    // Error
                    alert(err);
                });
            };


        }
    ]);


})(window.angular);