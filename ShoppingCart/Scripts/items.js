

function ItemsViewModel() {
    var self = this;

    self.items = ko.observableArray();

    $.getJSON('/api/ProductApi', function (response) {
        self.items(response);
    });
}
$(document).ready(function () {
    var viewmodel = new ItemsViewModel();
    ko.applyBindings(viewmodel);
});