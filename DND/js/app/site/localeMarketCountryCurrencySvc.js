(function () {
    window.app.factory('localeMarketCurrencySvc', localeMarketCurrencySvc);
    
    localeMarketCurrencySvc.$inject = ['$cookies', '$window', '$httpParamSerializer', '$rootScope', '$http'];
    function localeMarketCurrencySvc($cookies, $window, $httpParamSerializer, $rootScope, $http) {
        var locales = [];
        var markets = [];
        var currencies = [];

        initialise();

        var svc = {
            locales : locales,
            markets: markets,
            currencies: currencies,
            getLocales: getLocales,
            getMarkets: getMarkets,
            getMarket: getMarket,
            getCurrencies: getCurrencies,
            getCurrency : getCurrency,
            loadMarkets: loadMarkets
        };

        return svc;

        function initialise() {
            loadLocales();
            loadMarkets($rootScope.locale);
            loadCurrencies();
        }

        function loadLocales() {
            getLocales().success(function (data) {
                 locales.length = 0;
                 locales.addRange(data);
             });
        }

        function loadMarkets(locale) {
            getMarkets(locale).success(function (data) {
                markets.length = 0;
                markets.addRange(data);
            });
        }

        function loadCurrencies() {
            getCurrencies().success(function (data) {
                currencies.length = 0;
                currencies.addRange(data);
            });
        }

        function getLocales() {
            return $http.get('/api/locale');
        }

        function getMarkets(locale) {
           return $http.get('/api/market/by-locale/' + locale);
        }

        function getMarket(id) {
            for (var i = 0; i < markets.length; i++) {
                if (markets[i].id == id) return markets[i];
            }

            return null;
        }

        function getCurrencies() {
            return $http.get('/api/currency');
        }

        function getCurrency(id) {
            for (var i = 0; i < currencies.length; i++) {
                if (currencies[i].id == id) return currencies[i];
            }

            return null;
        }

    }
})();