(function () {
    "use strict";

    window.app.directive('itineraryMap', itineraryMap);

    function itineraryMap() {
        return {
            scope: {
                itinerary: "="
            },
            templateUrl: '/flightSearch/template/itineraryMap.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['$scope', 'NgMap'];
    function controller($scope, NgMap) {
        var vm = this;
        vm.itinerary = $scope.itinerary;      

        NgMap.getMap().then(function (map) {
            console.log(map.getCenter());
            console.log('markers', map.markers);
            console.log('shapes', map.shapes);
        });

        function getCenterPosition(list) {

            var latitudearray = [];
            var longitudearray = [];
            for (var i = 0; i < list.length; i++) {
                var coordinates = list[i];
                latitudearray.push(coordinates['latitude']);
                longitudearray.push(coordinates['longitude']);
            }

            latitudearray.sort(function (a, b) { return a - b; });
            longitudearray.sort(function (a, b) { return a - b; });
            var latdifferenece = latitudearray[latitudearray.length - 1] - latitudearray[0];
            var temp = (latdifferenece / 2).toFixed(4);
            var latitudeMid = parseFloat(latitudearray[0]) + parseFloat(temp);
            var longidifferenece = longitudearray[longitudearray.length - 1] - longitudearray[0];
            temp = (longidifferenece / 2).toFixed(4);
            var longitudeMid = parseFloat(longitudearray[0]) + parseFloat(temp);
            var maxdifference = (latdifferenece > longidifferenece) ? latdifferenece : longidifferenece;
            var zoomvalue;
            if (maxdifference >= 0 && maxdifference <= 0.0037)  //zoom 17
                zoomvalue = '17';
            else if (maxdifference > 0.0037 && maxdifference <= 0.0070)  //zoom 16
                zoomvalue = '16';
            else if (maxdifference > 0.0070 && maxdifference <= 0.0130)  //zoom 15
                zoomvalue = '15';
            else if (maxdifference > 0.0130 && maxdifference <= 0.0290)  //zoom 14
                zoomvalue = '14';
            else if (maxdifference > 0.0290 && maxdifference <= 0.0550)  //zoom 13
                zoomvalue = '13';
            else if (maxdifference > 0.0550 && maxdifference <= 0.1200)  //zoom 12
                zoomvalue = '12';
            else if (maxdifference > 0.1200 && maxdifference <= 0.4640)  //zoom 10
                zoomvalue = '10';
            else if (maxdifference > 0.4640 && maxdifference <= 1.8580)  //zoom 8
                zoomvalue = '8';
            else if (maxdifference > 1.8580 && maxdifference <= 3.5310)  //zoom 7
                zoomvalue = '7';
            else if (maxdifference > 3.5310 && maxdifference <= 7.3367)  //zoom 6
                zoomvalue = '6';
            else if (maxdifference > 7.3367 && maxdifference <= 14.222)  //zoom 5
                zoomvalue = '5';
            else if (maxdifference > 14.222 && maxdifference <= 28.000)  //zoom 4
                zoomvalue = '4';
            else if (maxdifference > 28.000 && maxdifference <= 58.000)  //zoom 3
                zoomvalue = '3';
            else
                zoomvalue = '1';

            return { latitude: latitudeMid, longitude: longitudeMid, zoom: zoomvalue };
        }
       
    }


})();