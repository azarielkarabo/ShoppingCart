

function ItemsViewModel() {
    var self = this;

    self.items = ko.observableArray();
    self.categories = ko.observableArray();

    function nodeModel(data) {
        data.price = 'R ' + data.price;
        data.addToCart = function (model) {
            $.post("api/v2/Folders/", model).then(function (response) {

            });
        };

        return data;
    }

    $.getJSON('/api/ProductApi', function (response) {

        self.items($.map(response, function (resp) {
            return new nodeModel(resp);
        }));

    });
    $.getJSON('/api/CategoryApi/Pull', function (response) {
        self.categories(response);
    });
}
$(document).ready(function () {
    var viewmodel = new ItemsViewModel();
    ko.applyBindings(viewmodel);
});