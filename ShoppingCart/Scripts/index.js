

function ItemsViewModel() {
    var self = this;

    self.items = ko.observableArray();
    self.filteredItems = ko.observableArray();
    self.cartItems = ko.observableArray();
    self.orders = ko.observableArray();
    self.categories = ko.observableArray();

    //Used for editing on the modal
    self.category = ko.observable();
    self.product = ko.observable();


    self.itemsCount = ko.observable();
    self.totalProducts = ko.observable();

    self.total = ko.observable();
    var totalProducts = 0;

    var total = 0.0;

    //contact properties
    self.firstName = ko.observable();
    self.lastName = ko.observable();
    self.email = ko.observable();
    self.cellNumber = ko.observable();
    self.message = ko.observable();

    //customer order page props
    self.cfirstName = ko.observable();
    self.clastName = ko.observable();
    self.caddress = ko.observable();
    self.ccity = ko.observable();
    self.cstate = ko.observable();
    self.cpostalCode = ko.observable();
    self.ccountry = ko.observable();
    self.cphone = ko.observable();
    self.cemail = ko.observable();
    self.ctotal = ko.observable();
    self.chasBeenShipped = ko.observable();

    //product properties on admin page
    self.prodName = ko.observable();
    self.prodDescription = ko.observable();
    self.prodCategory = ko.observable();
    self.prodPrice = ko.observable();

    //category properties on admin page
    self.categoryName = ko.observable();
    self.categoryDescription = ko.observable();

    //property used to search products
    self.searchText = ko.observable();

    //models that helps with the functions that are found in the list
    //they even modify the objects that comes from the server

    function nodeModel(data) {

        data = ko.mapping.fromJS(data);

        data.price('R ' + data.price());
        var path = data.imagePath() === null ? '/Content/Images/steak.jpeg' : data.imagePath();
        data.imagePath(path);
        data.addToCart = function (model) {

            var cartItem = {
                ProductId: model.id(),
                Quantity: 1
            };

            $.post("api/CartItemApi", cartItem).then(function (response) {
                if (response.message) {
                    alert(response.message);
                    window.location.href = "/Account/Login";
                } else {
                    self.cartItems.push(response);
                    self.itemsCount(self.itemsCount() + 1);
                }

            });
        };
        data.remove = function (model) {
            if (model) {
                $.ajax({
                    url: '/api/ProductApi/' + model.id(),
                    type: 'DELETE',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'json',
                    data: {},
                    success: function (response) {
                        if (response.message) {
                            alert(response.message);
                        } else {
                            self.filteredItems.remove(model);
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

    function cartModel(data) {

        data = ko.mapping.fromJS(data);

        total = total + data.unitPrice();
        totalProducts = totalProducts + 1;

        self.total(self.toPrice(total));
        self.totalProducts(totalProducts);

        data.remove = function (model) {
            if (model) {
                $.ajax({
                    url: '/api/CartItemApi/' + model.id(),
                    type: 'DELETE',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'json',
                    data: {},
                    success: function (response) {
                        if (typeof response !== 'string') {
                            self.cartItems.remove(model);
                        }
                    }
                });
            }
        };

        data.add = function (model) {
            var cartItem = {
                ProductId: model.productId(),
                Quantity: 1
            };
            $.post("/api/CartItemApi", cartItem).then(function (response) {
                if (response.message) {
                    alert(response.message);
                } else {

                    ko.utils.arrayForEach(self.cartItems(), function (item) {
                        if (item.productId() === response.productId) {
                            item.quantity(response.quantity);
                            item.unitPrice(response.unitPrice);
                        }
                        self.calculateSum();
                    });

                    self.itemsCount(self.itemsCount() + 1);
                }
            });
        };

        data.subtract = function (model) {
            var cartItem = {
                ProductId: model.productId(),
                Quantity: -1
            };
            if (model.quantity() <= 1) {
                alert('You cannot subtract quantity to 0 ,rather click on the remove button');
            } else {
                $.post("/api/CartItemApi", cartItem).then(function (response) {
                    if (response.message) {
                        alert(response.message);
                    } else {
                        ko.utils.arrayForEach(self.cartItems(), function (item) {
                            if (item.productId() === response.productId) {
                                item.quantity(response.quantity);
                                item.unitPrice(response.unitPrice);
                            }
                        });
                        self.calculateSum();
                        self.itemsCount(self.itemsCount() - 1);
                    }
                });
            }
        };

        return data;
    }

    function orderModel(data) {

        data = ko.mapping.fromJS(data);

        data.details = function (data) {
            alert("Details to be added later");
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
                    url: '/api/CategoryApi/' + model.id(),
                    type: 'DELETE',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'json',
                    data: {},
                    success: function (response) {
                        if (response.message) {
                            alert(response.message);
                        } else {
                            self.categories.remove(model);
                        }
                    }
                });
            }
        };

        return data;

    }

    //functions
    self.toPrice = function (value) {
        var price = value === null ? 0.0 : value;
        return 'R ' + price.toFixed(3);
    };

    self.calculateSum = function () {
        var sum = 0.0;
        ko.utils.arrayForEach(self.cartItems(), function (item) {
            return sum = sum + item.unitPrice();
        });

        self.total(self.toPrice(sum));
    };

    self.clearCart = function () {

        $.ajax({
            url: '/api/CartItemApi/ClearCart',
            type: 'DELETE',
            contentType: 'application/json;charset=utf-8',
            dataType: 'json',
            data: {},
            success: function (response) {
                if (response.message) {
                    alert(response.message);
                    window.location.href = "/Account/Login";
                } else {
                    self.cartItems.removeAll();
                    self.total(self.toPrice(0.0));
                    self.itemsCount(0);
                }
            }
        });
    };

    self.countItems = function () {

        var quantitiesCount = 0;
        ko.utils.arrayForEach(self.cartItems(), function (item) {

            return quantitiesCount = quantitiesCount + item.quantity();
        });

        self.itemsCount(quantitiesCount);

    };

    self.sendMessage = function () {

        var contact = {
            firstName: self.firstName(),
            lastName: self.lastName(),
            email: self.email(),
            cellNumber: self.cellNumber(),
            message: self.message()
        };

        $.post("/api/ContactApi", contact).then(function (response) {
            if (response.message) {
                alert(response.message);
            } else {

                $('#exampleModalLong').modal('show');
                self.cleanContact();
            }

        });

    };

    self.placeOrder = function () {

        var orderVm = {
            firstName: self.cfirstName(),
            lastName: self.clastName(),
            address: self.caddress(),
            city: self.ccity(),
            state: self.cstate(),
            postalCode: self.cpostalCode(),
            country: self.ccountry(),
            phone: self.cphone(),
            email: self.cemail(),
            total: self.ctotal(),
            hasBeenShipped: self.chasBeenShipped()
        };


        $.post('/api/OrderApi', orderVm).then(function (response) {
            if (response.message) {
                alert(response.message);
            } else {
                self.cleanCustomer();
                alert("Order is placed successfully !");
                window.location.href = "/Home/Index";
            }
        });

    };

    self.cleanContact = function () {
        self.firstName(null);
        self.lastName(null);
        self.email(null);
        self.cellNumber(null);
        self.message(null);
    };

    self.cleanCustomer = function () {
        self.cfirstName(null);
        self.clastName(null);
        self.caddress(null);
        self.ccity(null);
        self.cstate(null);
        self.cpostalCode(null);
        self.ccountry(null);
        self.cphone(null);
        self.cemail(null);
        self.ctotal(null);
        self.chasBeenShipped(null);
    };


    //events
    self.searchText.subscribe(function (value) {

        if (value) {
            value = value.toLowerCase();
            var filteredItems = ko.utils.arrayFilter(self.items(), function (item) {
                return item.name().toLowerCase().includes(value) || (item.description() !== null && item.description().toLowerCase().includes(value));
            });

            self.filteredItems(filteredItems);
        } else {
            self.filteredItems(self.items());
        }
    });

    //Data requests

    //It will be ran when we at home page or admin page
    if ($('#listItems').length > 0 || $('#adminView').length > 0) {
        $.getJSON('/api/ProductApi', function (response) {

            //This helps when we get authorization errors
            if (response.message) {
                alert(response.message);
            } else {
                self.items($.map(response, function (resp) {
                    return new nodeModel(resp);
                }));

                //Initializes the second array
                self.filteredItems(self.items());
            }
        });
    }

    //It will be ran when we on admin view
    if ($('#adminView').length > 0) {

        $.getJSON('/api/OrderApi', function (response) {
            if (response.message) {
                alert(response.message);
            } else {
                self.orders($.map(response, function (resp) {
                    return new orderModel(resp);
                }));
            }
        });

        $.getJSON('/api/CategoryApi', function (response) {
            //This helps when we get authorization errors
            if (response.message) {
                alert(response.message);
            } else {
                self.categories($.map(response, function (resp) {
                    return new categoryModel(resp);
                }));
            }
        });

    }

    //This is called on every page , on react it was gonna be called once and get stored on states or saggas
    $.getJSON('/api/CartItemApi/GetAll', function (response) {
        if (response.message) {
            //alert(response.message);
            //window.location.href = "/Account/Login";
        } else {
            self.cartItems($.map(response, function (resp) {
                return new cartModel(resp);
            }));

            self.countItems();
        }
    });

}
$(document).ready(function () {
    var viewmodel = new ItemsViewModel();
    ko.applyBindings(viewmodel);
});