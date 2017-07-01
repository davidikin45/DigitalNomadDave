(function () {
    "use strict";


    window.app.directive('flightSearch', flightSearch);

    function flightSearch() {
        return {
            templateUrl: '/flightSearch/template/flightSearch.tmpl.cshtml',
            controller: FlightSearchController,
            bindToController: true,
            scope: true,
            controllerAs: 'vm'
        }
    }

    window.app.controller('FlightSearchController', FlightSearchController);

    FlightSearchController.$inject = ['$cookies', '$window', '$filter', '$rootScope', '$scope', '$uibModal', 'flightSearchSvc'];

    function FlightSearchController($cookies, $window, $filter, $rootScope, $scope, $uibModal, flightSearchSvc) {
        var vm = this;
        vm.search = search;
        vm.isLoading = false;
        vm.openFilterModal = openFilterModal;
        vm.closeFilterModal = closeFilterModal;
        vm.openSortModal = openSortModal;
        vm.closeSortModal = closeSortModal;
        vm.resetSort = resetSort;
        vm.resetFilters = resetFilters;
        vm.selectedView = 'advancedSearch';
        vm.setView = setView;

        vm.originLocation = '';
        vm.destinationLocation = '';

        
        function init() {
            if (SearchOccured()) {

                flightSearchSvc.location({ id: vm.flightSearch.originLocation.placeId }).then(function (response) {
                    vm.originLocation = response.data.placeName;
                });

                flightSearchSvc.location({ id: vm.flightSearch.destinationLocation.placeId }).then(function (response) {
                    vm.destinationLocation = response.data.placeName;
                });

                vm.selectedView = 'simpleSearch';
            }
            else {
                vm.selectedView = 'advancedSearch';
            }
        }

        function setView(view) {
            vm.selectedView = view;
        }

        vm.optionsPrice = {
            floor: 0,
            ceil: 1000,
            translate: function (value, sliderId, label) {
                switch (label) {
                    //case 'model':
                    //   return '<b>Min price per Adult:</b> ' + value;
                    case 'high':
                        if (value == vm.optionsPrice.ceil) {
                            return value + '+';
                        }
                        else {
                            return '' + value;
                        }
                    case 'ceil':
                        return value + '+';
                    default:
                        return '' + value
                }
            }
        }


        function SearchOccured() {
            var pairs = location.hash.slice(1).split('&');

            return pairs.length > 1;
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
                    case 'high':
                        if (value == vm.optionsTimeOutboundArrival.ceil) {
                            return HRSMINS + '+';
                        }
                        else {
                            return '' + HRSMINS;
                        }
                    case 'ceil':
                        return HRSMINS + '+';
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
                    case 'high':
                        if (value == vm.optionsTimeOutboundArrival.ceil) {
                            return HRSMINS + '+';
                        }
                        else {
                            return '' + HRSMINS;
                        }
                    case 'ceil':
                        return HRSMINS + '+';
                    default:
                        return '' + HRSMINS
                }
            }
        }

        var cookieFlightSearch = $cookies.getObject('flightSearch');

        if (cookieFlightSearch) {
            vm.flightSearch = cookieFlightSearch;
            vm.flightSearch.outboundDate = new Date(vm.flightSearch.outboundDate);
            vm.flightSearch.inboundDate = new Date(vm.flightSearch.inboundDate);
        }
        else {
            resetSearch();
        }
        init();
        function resetSearch() {
            vm.flightSearch = {
                originLocation: { placeId: 'nearest', placeName: 'Nearest Airport' },
                returnFlight: true,
                adults: '1',
                children: '0',
                infants: '0',
                flightClass: 'Economy',
                directFlightsOnly: true,
                outboundDate: new Date(),
                inboundDate: new Date(Date.now() + 1 * 24 * 60 * 60 * 1000)
            };
            resetFilters();
            resetSort();
        }

        function resetFilters() {

            vm.flightSearch.priceFilterPerAdultMin = vm.optionsPrice.floor;
            vm.flightSearch.priceFilterPerAdultMax = vm.optionsPrice.ceil;
            vm.flightSearch.outboundDepartureTimeFrom = vm.optionsTimeOutboundDeparture.floor;
            vm.flightSearch.outboundDepartureTimeTo = vm.optionsTimeOutboundDeparture.ceil;
            vm.flightSearch.outboundArrivalTimeFrom = vm.optionsTimeOutboundArrival.floor;
            vm.flightSearch.outboundArrivalTimeTo = vm.optionsTimeOutboundArrival.ceil;
            vm.flightSearch.inboundDepartureTimeFrom = vm.optionsTimeInboundDeparture.floor;
            vm.flightSearch.inboundDepartureTimeTo = vm.optionsTimeInboundDeparture.ceil;
            vm.flightSearch.inboundArrivalTimeFrom = vm.optionsTimeInboundArrival.floor;
            vm.flightSearch.inboundArrivalTimeTo = vm.optionsTimeInboundArrival.ceil;
            vm.flightSearch.outboundDurationMin = vm.optionsTimeOutboundArrival.floor;
            vm.flightSearch.outboundDurationMax = vm.optionsTimeOutboundArrival.ceil;
            vm.flightSearch.inboundDurationMin = vm.optionsTimeInboundArrival.floor;
            vm.flightSearch.inboundDurationMax = vm.optionsTimeInboundArrival.ceil;
        }

        function resetSort() {
            vm.flightSearch.sortType = 'Price';
            vm.flightSearch.sortOrder = 'Asc';
        }

        function openFilterModal() {
            var modalInstance = $uibModal.open({
                templateUrl: '/flightsearch/template/flightSearchFilters.tmpl.cshtml',
                scope: $scope,
                controller: FlightSearchController,
                controllerAs: vm
            });
            modalInstance.rendered.then(function () {
                $rootScope.$broadcast('rzSliderForceRender'); //Force refresh sliders on render. Otherwise bullets are aligned at left side.
            });

        }

        function closeFilterModal() {
            if (SearchOccured()) {
                search();
            }
        }

        function openSortModal() {
            var modalInstance = $uibModal.open({
                templateUrl: '/flightsearch/template/flightSearchSort.tmpl.cshtml',
                scope: $scope,
                controller: FlightSearchController,
                controllerAs: vm
            });
        }

        function closeSortModal() {
            if (SearchOccured()) {
                search();
            }          
        }

        var _selected;
        vm.optionSelected = function (value) {
            if (arguments.length) {
                _selected = value;
            } else {
                return _selected;
            }
        };

        vm.modelOptions = {
            debounce: {
                default: 500,
                blur: 250
            },
            getterSetter: true
        };

        vm.getLocation = function (val) {
            return flightSearchSvc.locationAutoSuggest({ search: val }).then(function (response) {
                return response.data.locations;
            });
        };

        vm.errorMessage = null;

        $rootScope.$on('search', function (event, data) {
            if (SearchOccured())
            {
                search();
            }
        });

        function search() {
            vm.saving = true;

            var now = new Date(),
            exp = new Date(now.getFullYear(), now.getMonth(), now.getDate() + 1);
            $cookies.putObject('flightSearch', vm.flightSearch, { expires: exp });

            //$cookies.putObject('flightSearch', vm.flightSearch);

            var clientSearch = {};

            clientSearch.returnFlight = vm.flightSearch.returnFlight;

            clientSearch.originLocation = vm.flightSearch.originLocation.placeId;
            clientSearch.destinationLocation = vm.flightSearch.destinationLocation.placeId;

            clientSearch.outboundDate = $filter('date')(new Date(vm.flightSearch.outboundDate), 'yyyy-MM-dd');

            if (vm.flightSearch.outboundDepartureTimeFrom) {
                clientSearch.outboundDepartureTimeFrom = vm.flightSearch.outboundDepartureTimeFrom;
            }

            if (vm.flightSearch.outboundDepartureTimeTo != vm.optionsTimeOutboundDeparture.ceil) {
                clientSearch.outboundDepartureTimeTo = vm.flightSearch.outboundDepartureTimeTo;
            }

            if (vm.flightSearch.outboundArrivalTimeFrom) {
                clientSearch.outboundArrivalTimeFrom = vm.flightSearch.outboundArrivalTimeFrom;
            }

            if (vm.flightSearch.outboundArrivalTimeTo && vm.flightSearch.outboundArrivalTimeTo < vm.optionsTimeOutboundArrival.ceil) {
                clientSearch.outboundArrivalTimeTo = vm.flightSearch.outboundArrivalTimeTo;
            }

            if (vm.flightSearch.outboundDurationMin) {
                clientSearch.outboundDurationMin = vm.flightSearch.outboundDurationMin;
            }

            if (vm.flightSearch.outboundDurationMax && vm.flightSearch.outboundDurationMax < vm.optionsTimeOutboundArrival.ceil) {
                clientSearch.outboundDurationMax = vm.flightSearch.outboundDurationMax;
            }


            if (clientSearch.returnFlight) {
                clientSearch.inboundDate = $filter('date')(new Date(vm.flightSearch.inboundDate), 'yyyy-MM-dd');

                if (vm.flightSearch.inboundDepartureTimeFrom) {
                    clientSearch.inboundDepartureTimeFrom = vm.flightSearch.inboundDepartureTimeFrom;
                }

                if (vm.flightSearch.inboundDepartureTimeTo) {
                    clientSearch.inboundDepartureTimeTo = vm.flightSearch.inboundDepartureTimeTo;
                }

                if (vm.flightSearch.inboundArrivalTimeFrom) {
                    clientSearch.inboundArrivalTimeFrom = vm.flightSearch.inboundArrivalTimeFrom;
                }

                if (vm.flightSearch.inboundArrivalTimeTo && vm.flightSearch.inboundArrivalTimeTo < vm.optionsTimeInboundArrival.ceil) {
                    clientSearch.inboundArrivalTimeTo = vm.flightSearch.inboundArrivalTimeTo;
                    //$filter('date')(new Date(vm.flightSearch.inboundArrivalTimeTo), 'HH:mm');
                }

                if (vm.flightSearch.inboundDurationMin) {
                    clientSearch.inboundDurationMin = vm.flightSearch.inboundDurationMin;
                }

                if (vm.flightSearch.inboundDurationMax && vm.flightSearch.inboundDurationMax < vm.optionsTimeInboundArrival.ceil) {
                    clientSearch.inboundDurationMax = vm.flightSearch.inboundDurationMax;
                }
            }


            clientSearch.adults = vm.flightSearch.adults;
            clientSearch.children = vm.flightSearch.children;
            clientSearch.infants = vm.flightSearch.infants;

            clientSearch.flightClass = vm.flightSearch.flightClass;

            clientSearch.directFlightsOnly = vm.flightSearch.directFlightsOnly;

            if (vm.flightSearch.priceFilterPerAdultMin > vm.optionsPrice.floor) {
                clientSearch.priceFilterPerAdultMin = vm.flightSearch.priceFilterPerAdultMin;
            }

            if (vm.flightSearch.priceFilterPerAdultMax < vm.optionsPrice.ceil) {
                clientSearch.priceFilterPerAdultMax = vm.flightSearch.priceFilterPerAdultMax;
            }

            clientSearch.sortType = vm.flightSearch.sortType;
            clientSearch.sortOrder = vm.flightSearch.sortOrder;

            flightSearchSvc.searchRedirect(clientSearch);
            init();
        }
    }
})();