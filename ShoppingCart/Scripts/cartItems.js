
function cartItemsVm() {
    var self = this;

    self.items = ko.observableArray();
    self.products = ko.observableArray();
    self.product = ko.observable();

    self.categories = ko.observableArray();
    self.prodName = ko.observable();
    self.prodDescription = ko.observable();
    self.prodCategory = ko.observable();
    self.prodPrice = ko.observable();

    self.categoryName = ko.observable();
    self.categoryDescription = ko.observable();

    function productModel(data) {

        data = ko.mapping.fromJS(data);

        var price = data.price() === null ? 0.0 : data.price();

        data.isEditing = ko.observable(false);
        data.formattedPrice = ko.observable('R ' + price);
        data.remove = function (model) {

            if (model)
                self.product(model);
            if (model) {
                $.ajax({
                    url: '/api/ProductApi/' + self.product().id(),
                    type: 'DELETE',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'json',
                    data: {},
                    success: function (response) {
                        if (typeof response !== 'string') {
                            self.products.remove(model);
                            self.product(null);
                        }
                    }
                });
            }
        };
        data.edit = function (model) {
            data.isEditing(true);
            self.product(model);
        };

        return data;
    }

    function categoryModel(data) {
        return data;
    }

    $.getJSON('/api/CartItemApi/GetAll', function (response) {

        //This helps when we get authorization errors
        if (typeof response !== 'string') {
            self.items(response);
        }
    });

    $.getJSON('/api/ProductApi', function (response) {

        //This helps when we get authorization errors
        if (typeof response !== 'string') {
            self.products($.map(response, function (resp) {
                return new productModel(resp);
            }));
        }
    });

    $.getJSON('/api/CategoryApi', function (response) {
        //This helps when we get authorization errors
        if (typeof response !== 'string') {
            self.categories($.map(response, function (resp) {
                return new categoryModel(resp);
            }));
        }
    });

    self.addProduct = function () {

        var productVm = {
            name: self.prodName(),
            categoryId: self.prodCategory(),
            price: self.prodPrice(),
            description: self.prodDescription()
        };

        $.post('/api/ProductApi', productVm).then(function (response) {
            if (typeof response !== 'string') {
                self.products.push(new productModel(response));
                $('#productModal').modal('hide');
            }
        });
    };
    self.saveProduct = function () {

        var data = JSON.stringify({
            Id: self.product().id(),
            Name: self.product().name(),
            Description: self.product().description(),
            Price: self.product().price()
        });

        $.ajax({
            url: '/api/ProductApi/' + self.product().id(),
            type: 'PUT',
            contentType: 'application/json;charset=utf-8',
            dataType: 'json',
            data: data,
            success: function (response) {
                if (typeof response !== 'string') {
                    ko.utils.arrayForEach(self.products(), function (item) {
                        if (item.id() === response.id) {
                            item.name(response.name);
                            item.description(response.description);
                            item.price(response.price);
                        }
                    });
                }
                $('#productEditModal').modal('hide');
            }
        });
    };

    self.addCategory = function () {

        var categoryVm = {
            name: self.categoryName(),
            categoryId: self.categoryDescription()
        };

        $.post('/api/CategoryApi', categoryVm).then(function (response) {
            if (typeof response !== 'string') {
                self.products.push(new categoryModel(response));
            }
        });
    };
}


$(document).ready(function () {
    var viewmodel = new cartItemsVm();
    ko.applyBindings(viewmodel);
})