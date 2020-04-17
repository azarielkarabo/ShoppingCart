function summaryVm() {
    var self = this;

    self.items = ko.observableArray();
    self.total = ko.observable();
    self.totalQuantities = ko.observable();
    self.totalProducts = ko.observable();

    var total = 0.0;
    var totalQuantities = 0;
    var totalProducts = 0;

    self.firstName = ko.observable();
    self.lastName = ko.observable();
    self.address = ko.observable();
    self.city = ko.observable();
    self.state = ko.observable();
    self.postalCode = ko.observable();
    self.country = ko.observable();
    self.phone = ko.observable();
    self.email = ko.observable();
    self.total = ko.observable();
    self.hasBeenShipped = ko.observable();

    function summaryModel(data) {
        data = ko.mapping.fromJS(data);

        //calculations
        total = total + data.unitPrice();
        totalQuantities = totalQuantities + data.quantity();
        totalProducts = totalProducts + 1;

        self.total(self.toPrice(total));
        self.totalQuantities(totalQuantities);
        self.totalProducts(totalProducts);

        return data;
    }
    self.toPrice = function (value) {
        var price = value === null ? 0.0 : value;
        return 'R ' + price.toFixed(3);
    };
    $.getJSON('/api/CartItemApi/GetAll', function (response) {

        //This helps when we get authorization errors
        if (response.message) {
            alert(response.message);
            window.location.href = "/Account/Login";
        } else {
            self.items($.map(response, function (resp) {
                return new summaryModel(resp);
            }));
        }
    });

    self.placeOrder = function () {

        var orderVm = {
            firstName: self.firstName(),
            lastName: self.lastName(),
            address: self.address(),
            city: self.city(),
            state: self.state(),
            postalCode: self.postalCode(),
            country: self.country(),
            phone: self.phone(),
            email: self.email(),
            total: self.total(),
            hasBeenShipped: self.hasBeenShipped()
        };


        $.post('/api/OrderApi', orderVm).then(function (response) {
            if (response.message) {
                alert(response.message);
            } else {
                self.clean();
                alert("Order is placed successfully !");
                window.location.href = "/Home/Index";
            }
        });

    };

    self.clean = function () {
        self.firstName(null);
        self.lastName(null);
        self.address(null);
        self.city(null);
        self.state(null);
        self.postalCode(null);
        self.country(null);
        self.phone(null);
        self.email(null);
        self.total(null);
        self.hasBeenShipped(null);
    }

};
$(document).ready(function () {
    var vm = new summaryVm();
    ko.applyBindings(vm);
});