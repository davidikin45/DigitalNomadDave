(function () {
    "use strict";

    window.app.directive('flightSearchExplore', flightSearchExplore);

    function flightSearchExplore() {
        return {
            templateUrl: '/flightSearch/template/flightSearchExplore.tmpl.cshtml',
            controller: controller,
            controllerAs: 'vm'
        }
    }

    controller.$inject = ['flightSearchSvc', '$httpParamSerializer', '$uibModal', '$scope', '$rootScope'];
    function controller(flightSearchSvc, $httpParamSerializer, $uibModal, $scope, $rootScope) {
        var vm = this;
        vm.search = search;
        vm.isLoading = false;
        vm.errorMessage = null;
        vm.take = 10;
        vm.loadMore = loadMore;
        vm.openFilters = openFilters;

        var searchObject = HashToJSON();

        if (searchObject) {
            vm.flightSearch = searchObject;
            vm.flightSearch.outboundDate = new Date(vm.flightSearch.outboundDate);
            vm.flightSearch.inboundDate = new Date(vm.flightSearch.inboundDate);
        }
        else {
            vm.flightSearch = {
                returnFlight: true,
                adults: '1',
                children: '0',
                infants: '0',
                flightClass: '0',
                directFlightsOnly: true,
                priceFilterPerAdultMin: 0,
                priceFilterPerAdultMax: 1000,
                outboundDepartureTimeFrom: 0,
                outboundDepartureTimeTo: 1440,
                outboundArrivalTimeFrom: 0,
                outboundArrivalTimeTo: 1440,
                inboundDepartureTimeFrom: 0,
                inboundDepartureTimeTo: 1440,
                inboundArrivalTimeFrom: 0,
                inboundArrivalTimeTo: 1440,
                outboundDurationMin: 0,
                outboundDurationMax: 1440,
                inboundDurationMin: 0,
                inboundDurationMax: 1440
            };
        }

        search();

        function HashToJSON() {
            var pairs = location.hash.slice(1).split('&');

            var result = {};
            pairs.forEach(function (pair) {
                pair = pair.split('=');
                result[pair[0]] = decodeURIComponent(pair[1] || '');
            });

            return JSON.parse(JSON.stringify(result));
        }

        $(window).on('hashchange', function () {
            search();
        });

        vm.optionsPrice = {
            floor: 0,
            ceil: 1000,
            translate: function (value, sliderId, label) {
                switch (label) {
                    //case 'model':
                    //    return '<b>Min price per Adult:</b> ' + value;
                    //case 'high':
                    //    return '<b>Max price per Adult:</b> ' + value;
                    default:
                        return '' + value
                }
            }
        }

        vm.optionsTimeOutboundDeparture = {
            floor: 0,
            ceil: 1440,
            translate: function (value, sliderId, label) {

                var m = value % 60;
                var h = (value - m) / 60;
                var HRSMINS = h + ":" + (m < 10 ? "0" : "") + m;

                switch (label) {
                    //case 'model':
                    //    return '<b>Earliest departure time:</b> ' + HRSMINS;
                    //case 'high':
                    //    return '<b>Latest departure time:</b> ' + HRSMINS;
                    default:
                        return '' + HRSMINS
                }
            }
        }

        vm.optionsTimeOutboundArrival = {
            floor: 0,
            ceil: 1440,
            translate: function (value, sliderId, label) {

                var m = value % 60;
                var h = (value - m) / 60;
                var HRSMINS = h + ":" + (m < 10 ? "0" : "") + m;

                switch (label) {
                    //case 'model':
                    //    return '<b>Earliest arrival time:</b> ' + HRSMINS;
                    //case 'high':
                    //    return '<b>Latest arrival time:</b> ' + HRSMINS;
                    default:
                        return '' + HRSMINS
                }
            }
        }

        vm.optionsTimeInboundDeparture = {
            floor: 0,
            ceil: 1440,
            translate: function (value, sliderId, label) {

                var m = value % 60;
                var h = (value - m) / 60;
                var HRSMINS = h + ":" + (m < 10 ? "0" : "") + m;

                switch (label) {
                    //case 'model':
                    //    return '<b>Earliest departure time:</b> ' + HRSMINS;
                    //case 'high':
                    //    return '<b>Latest departure time:</b> ' + HRSMINS;
                    default:
                        return '' + HRSMINS
                }
            }
        }

        vm.optionsTimeInboundArrival = {
            floor: 0,
            ceil: 1440,
            translate: function (value, sliderId, label) {

                var m = value % 60;
                var h = (value - m) / 60;
                var HRSMINS = h + ":" + (m < 10 ? "0" : "") + m;

                switch (label) {
                    //case 'model':
                    //    return '<b>Earliest arrival time:</b> ' + HRSMINS;
                    //case 'high':
                    //    return '<b>Latest arrival time:</b> ' + HRSMINS;
                    default:
                        return '' + HRSMINS
                }
            }
        }

        function loadMore()
        {
            vm.take += 10;
            var searchObject = HashToJSON();
            searchObject.take = vm.take;
            var qs = $httpParamSerializer(searchObject);
            location.hash = "#" + qs;
        }

        function openFilters() {
            var modalInstance = $uibModal.open({
                templateUrl: '/flightsearch/template/flightSearchFilters.tmpl.cshtml',
                scope: $scope,
                controller: controller,
                controllerAs: vm
            });
            modalInstance.rendered.then(function () {
                $rootScope.$broadcast('rzSliderForceRender'); //Force refresh sliders on render. Otherwise bullets are aligned at left side.
            });

        }

        function search() {
            vm.isLoading = true;
            var searchObject = HashToJSON();
            searchObject.skip = 0;
            searchObject.take = vm.take;

            //var searchObject = $location.search();
            flightSearchSvc.search(searchObject)
				.success(function (data) {
				    vm.searchResults = data;
				})
				.error(function (data) {
				    vm.errorMessage = 'There was a problem searching: ' + data;
				})
				.finally(function () {
				    vm.isLoading = false;
				});
        }
    }
})();