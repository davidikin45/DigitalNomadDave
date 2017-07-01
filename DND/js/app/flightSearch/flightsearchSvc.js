(function () {
    window.app.factory('flightSearchSvc', flightSearchSvc);

    flightSearchSvc.$inject = ['$cookies', '$window', '$httpParamSerializer', '$rootScope', '$http'];
    function flightSearchSvc($cookies, $window, $httpParamSerializer, $rootScope, $http) {

        var svc = {
            search: search,
            searchRedirect: searchRedirect,
            locationAutoSuggest: locationAutoSuggest,
            location: location
        };

        return svc;


        function locationAutoSuggest(request) {

            var localeMarketCurrency = $cookies.getObject('localeMarketCurrency');
            if (localeMarketCurrency) {
                request.locale = localeMarketCurrency.locale;
                request.market = localeMarketCurrency.market;
                request.currency = localeMarketCurrency.currency;
            }
           

            return $http.post('/api/v1.0/location/auto-suggest', request);
        }

        function location(request) {

            var localeMarketCurrency = $cookies.getObject('localeMarketCurrency');
            if (localeMarketCurrency) {
                request.locale = localeMarketCurrency.locale;
                request.market = localeMarketCurrency.market;
                request.currency = localeMarketCurrency.currency;
            }


            return $http.post('/api/v1.0/location', request);
        }

        function search(request) {

            var localeMarketCurrency = $cookies.getObject('localeMarketCurrency');
            if (localeMarketCurrency) {
                request.locale = localeMarketCurrency.locale;
                request.market = localeMarketCurrency.market;
                request.currency = localeMarketCurrency.currency;
            }
          
            return $http.post('/api/v1.0/flight-search', request);
        }


        function searchRedirect(request) {

            var localeMarketCurrency = $cookies.getObject('localeMarketCurrency');
            if (localeMarketCurrency) {
                request.locale = localeMarketCurrency.locale;
                request.market = localeMarketCurrency.market;
                request.currency = localeMarketCurrency.currency;
            }
           
            request.skip = 0;
            request.take = 10;

            var qs = $httpParamSerializer(request);

            var url = "http://" + $window.location.host + "/flight-search#" + qs;
            window.location.href = url;

            //window.location.href = "http://www.google.com";

            //return $http.post('/api/v1.0/flightsearch/search', request);
        }

        function currentLocation() {
            var options = {
                enableHighAccuracy: true
            };

            navigator.geolocation.getCurrentPosition(function (pos) {
                var position = new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude);
                return position;
            },
                    function (error) {
                        console.log('Unable to get location: ' + error.message);
                    }, options);
        }
    }
})();