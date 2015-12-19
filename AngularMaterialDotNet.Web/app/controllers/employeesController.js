angular.module('App.employeesControllers', [])
    .controller('employeesIndexController', function ($scope, $state, $window, Employee) {
        $scope.employees = Employee.query(); //fetch all employees. Issues a GET to /api/employees
        $scope.deleteEmployee = function (employee) { // Delete a employee. Issues a DELETE to /api/employees/:id
            employee.$delete(function () {
                var index = $scope.employees.indexOf(employee);
                $scope.employees.splice(index, 1);
                //$window.location.href = ''; //redirect to home
            });
        };
        $scope.showCalc = function (id) {
            $('.calc').hide('slow');
            $('#calc' + id).show('slow');
        };
        }).controller('employeesDetailsController', function ($scope, $stateParams, Employee) {
         $scope.employee = Employee.get({ id: $stateParams.employeeId }); //Get a single employee.Issues a GET to /api/employees/:id
    }).controller('employeesAddController', function ($scope, $state, $stateParams, Employee) {
        $scope.message = "";
        $scope.employee = new Employee();  //create new employee instance. Properties will be set via ng-model on UI

        $scope.addEmployee = function () { //create a new employee. Issues a POST to /api/employees
            $scope.employee.$save(function () {
                $state.go('employees'); // on success go back to home i.e. employees state.
            }, function (err) {
                $scope.message = err.data.message;
                debugger;
            });
        };
    }).controller('employeesEditController', function ($scope, $state, $stateParams, Employee) {
        $scope.message = "";
        $scope.editEmployee = function () { //Update the edited employee. Issues a PUT to /api/employees/:id
            $scope.employee.$update(function () {
                $state.go('employees'); // on success go back to home i.e. employees state.
            });
        };

        $scope.loadEmployee = function () { //Issues a GET request to /api/employees/:id to get a employee to update
            $scope.employee = Employee.get({ id: $stateParams.employeeId });
        };

        $scope.loadEmployee(); // Load a employee which can be edited on UI
    });