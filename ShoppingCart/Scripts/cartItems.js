
function cartItemsVm() {
    var self = this;

    self.items = ko.observableArray();
    self.total = ko.observable();

    var total = 0.0;

    function itemModel(data) {

        data = ko.mapping.fromJS(data);

        total = total + data.unitPrice();
        self.total(self.toPrice(total));

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
                            self.items.remove(model);
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
                ko.utils.arrayForEach(self.items(), function (item) {
                    if (item.productId() === response.productId) {
                        item.quantity(response.quantity);
                        item.unitPrice(response.unitPrice);
                    }
                    self.calculateSum();
                });
            });
        };

        data.subtract = function (model) {
            var cartItem = {
                ProductId: model.productId(),
                Quantity: -1
            };
            if (model.quantity() > 1) {
                $.post("/api/CartItemApi", cartItem).then(function (response) {

                    ko.utils.arrayForEach(self.items(), function (item) {
                        if (item.productId() === response.productId) {
                            item.quantity(response.quantity);
                            item.unitPrice(response.unitPrice);
                        }
                    });
                    self.calculateSum();
                });
            }
        };
        return data;
    }

    self.toPrice = function (value) {
        var price = value === null ? 0.0 : value;
        return 'R ' + price.toFixed(3);
    };

    self.calculateSum = function () {
        var sum = 0.0;
        ko.utils.arrayForEach(self.items(), function (item) {
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
                if (typeof response !== 'string') {
                    self.items.removeAll();
                    self.total(self.toPrice(0.0));
                }
            }
        });
    };

    $.getJSON('/api/CartItemApi/GetAll', function (response) {

        //This helps when we get authorization errors
        if (typeof response !== 'string') {
            self.items($.map(response, function (resp) {
                return new itemModel(resp);
            }));
        }
    });

}


$(document).ready(function () {
    var viewmodel = new cartItemsVm();
    ko.applyBindings(viewmodel);
})