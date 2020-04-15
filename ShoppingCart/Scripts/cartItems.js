
function cartItemsVm() {
    var self = this;

    self.items = ko.observableArray();
    self.products = ko.observableArray();
    self.filteredProducts = ko.observableArray();
    self.categories = ko.observableArray();


    self.product = ko.observable();
    self.category = ko.observable();


    self.prodName = ko.observable();
    self.prodDescription = ko.observable();
    self.prodCategory = ko.observable();
    self.prodPrice = ko.observable();

    self.categoryName = ko.observable();
    self.categoryDescription = ko.observable();

    self.searchText = ko.observable();

    function productModel(data) {

        data = ko.mapping.fromJS(data);

        var price = data.price() === null ? 0.0 : data.price();
        var path = data.imagePath() === null ? '/Content/Images/steak.jpeg' : data.imagePath();

        data.formattedPrice = ko.observable('R ' + price);
        data.imagePath(path);
        data.remove = function (model) {
            if (model) {
                $.ajax({
                    url: '/api/ProductApi/' + model.id(),
                    type: 'DELETE',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'json',
                    data: {},
                    success: function (response) {
                        if (typeof response !== 'string') {
                            self.filteredProducts.remove(model);
                        }
                    }
                });
            }
        };
        data.edit = function (model) {
            self.product(model);
        };

        return data;
    }

    function categoryModel(data) {
        data = ko.mapping.fromJS(data);
        data.edit = function (data) {
            self.category(data);
        };

        data.remove = function (model) {

            if (model) {
                $.ajax({
                    url: '/api/CategoryApi/' + model.id,
                    type: 'DELETE',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'json',
                    data: {},
                    success: function (response) {
                        if (typeof response !== 'string') {
                            self.categories.remove(model);
                        }
                    }
                });
            }
        };

        return data;

    }

    self.searchText.subscribe(function (value) {

        if (value) {
            value = value.toLowerCase();
            var filteredProducts = ko.utils.arrayFilter(self.products(), function (item) {
                return item.name().toLowerCase().includes(value) || (item.description() !== null && item.description().toLowerCase().includes(value));
            });

            self.filteredProducts(filteredProducts);
        } else {
            self.filteredProducts(self.products());
        }
    });

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

            //Initializes the second array
            self.filteredProducts(self.products());
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

        var data = ko.toJS({
            name: self.prodName(),
            categoryId: self.prodCategory().id(),
            price: self.prodPrice(),
            description: self.prodDescription()
        });

        var formData = new FormData();
        var file = document.getElementById("productPicture").files[0];
        formData.append("productPicture", file);

        if (data !== null)
            for (var key in data) {
                if (data[key] !== null)
                    formData.append(key, data[key]);
            }

        $.ajax({
            url: '/api/ProductApi',
            type: 'POST',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                var formattedProduct = new productModel(response);
                self.filteredProducts.push(formattedProduct);
                $('#productModal').modal('hide');
                self.prodCategory(null);
                $('#catName').val('');
                self.prodName(null);
                self.prodPrice(null);
                self.prodDescription(null);

                $('#productPicture').val('');
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
                    ko.utils.arrayForEach(self.filteredProducts(), function (item) {
                        if (item.id() === response.id) {
                            item.name(response.name);
                            item.description(response.description);
                            item.price(response.price);
                        }
                    });
                }
                self.product(null);
                $('#productEditModal').modal('hide');
            }
        });
    };

    self.addCategory = function () {

        var categoryVm = {
            name: self.categoryName(),
            description: self.categoryDescription()
        };

        $.post('/api/CategoryApi', categoryVm).then(function (response) {
            if (typeof response !== 'string') {
                self.categories.push(new categoryModel(response));
                $('#categoryModal').modal('hide');
                self.categoryName(null);
                self.categoryDescription(null);
            }
        });
    };
    self.saveCategory = function () {

        var category = JSON.stringify({
            name: self.category().name(),
            description: self.category().description()
        });

        $.ajax({
            url: '/api/CategoryApi/' + self.category().id(),
            type: 'PUT',
            contentType: 'application/json;charset=utf-8',
            dataType: 'json',
            data: category,
            success: function (response) {
                if (typeof response !== 'string') {
                    ko.utils.arrayForEach(self.categories(), function (item) {
                        if (item.id()=== response.id) {
                            item.name(response.name);
                            item.description(response.description);
                        }
                    });
                }
                self.category(null);
                $('#categoryEditModal').modal('hide');
            }
        });
       
    };
}


$(document).ready(function () {
    var viewmodel = new cartItemsVm();
    ko.applyBindings(viewmodel);
})