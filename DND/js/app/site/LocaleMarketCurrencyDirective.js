(function () {
    "use strict";

    window.app.directive('localeMarketCurrency', localeMarketCurrency);

    function localeMarketCurrency() {
        return {
            templateUrl: '/site/template/localeMarketCurrency.tmpl.cshtml',
            controller: LocaleMarketCurrencyController,
            bindToController: true,
            scope: true,
            controllerAs: 'vm'
        }
    }

    LocaleMarketCurrencyController.$inject = ['$cookies', '$window', '$filter', '$rootScope', '$scope', '$uibModal', 'localeMarketCurrencySvc'];
    function LocaleMarketCurrencyController($cookies, $window, $filter, $rootScope, $scope, $uibModal, localeMarketCurrencySvc) {
        var vm = this;
        vm.close = close;

        var localeMarketCurrency = $cookies.getObject('localeMarketCurrency');
        if (localeMarketCurrency) {
            vm.selectedLocaleId = localeMarketCurrency.locale;
            vm.selectedMarketIds = localeMarketCurrency.market;
            vm.selectedCurrencyId = localeMarketCurrency.currency;
        }

        vm.isLoading = false;

        vm.locales = localeMarketCurrencySvc.locales;
        vm.markets = localeMarketCurrencySvc.markets;
        vm.currencies = localeMarketCurrencySvc.currencies;

        vm.openLocaleMarketCurrencyModal = openLocaleMarketCurrencyModal;

        vm.onLocaleChange = onLocaleChange;
        vm.onMarketChange = onMarketChange;
        vm.onCurrencyChange = onCurrencyChange;

        function openLocaleMarketCurrencyModal() {
            var modalInstance = $uibModal.open({
                templateUrl: '/site/template/localeMarketCurrencyModal.tmpl.cshtml',
                scope: $scope,
                controller: LocaleMarketCurrencyController,
                controllerAs: vm
            });
        }

        function onLocaleChange() {
            localeMarketCurrencySvc.loadMarkets($rootScope.locale);
            save();
        }

        function onMarketChange() {
            if (vm.selectedMarketIds.length === 1) {
                var market = localeMarketCurrencySvc.getMarket(vm.selectedMarketIds[0]);
                if (market.currencyId && localeMarketCurrencySvc.getCurrency(market.currencyId)) {
                    vm.selectedCurrencyId = market.currencyId;
                    onCurrencyChange();
                }
            }
            save();
        }

        function onCurrencyChange() {
            save();
        }

        function SearchOccured() {
            var pairs = location.hash.slice(1).split('&');

            return pairs.length > 1;
        }

        function save() {
            var localeMarketCurrency = $cookies.getObject('localeMarketCurrency');
            if (!localeMarketCurrency) {
                localeMarketCurrency = new Object();
            }
            localeMarketCurrency.locale = vm.selectedLocaleId;
            localeMarketCurrency.market = vm.selectedMarketIds;
            localeMarketCurrency.currency = vm.selectedCurrencyId;
            $cookies.putObject('localeMarketCurrency', localeMarketCurrency);

        }

        function close()
        {
            $rootScope.$broadcast('search');
        }

    }
})();