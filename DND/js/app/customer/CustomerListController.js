(function() {
	'use strict';

	window.app.controller('CustomerListController', CustomerListController);

	CustomerListController.$inject = ['$uibModal', 'customerSvc'];
	function CustomerListController($uibModal, customerSvc) {
		var vm = this;
		vm.add = add;
		vm.customers = customerSvc.customers;


		function add() {
		    $uibModal.open({
		        template: '<add-customer />'
			});
		}
	}
})();