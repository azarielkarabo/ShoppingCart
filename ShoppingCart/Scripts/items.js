

function ItemsViewModel() {
    var self = this;

    self.items = ko.observableArray();
    self.categories = ko.observableArray();
    self.cartItems = ko.observableArray();

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
                } else {
                    self.cartItems.push(response);
                }

            });
        };

        return data;
    }

    $.getJSON('/api/ProductApi', function (response) {

        //This helps when we get authorization errors
        if (response.message) {
            alert(response.message);
        } else {
            self.items($.map(response, function (resp) {
                return new nodeModel(resp);
            }));
        }
    });
    $.getJSON('/api/CategoryApi/Pull', function (response) {
        if (response.message) {
            alert(response.message);
        } else {
            self.categories(response);
        }
    });
}
$(document).ready(function () {
    var viewmodel = new ItemsViewModel();
    ko.applyBindings(viewmodel);
});