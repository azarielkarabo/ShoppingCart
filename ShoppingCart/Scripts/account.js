function accountViewModel() {
    var self = this;

}

$(document).ready(function () {
    var viewmodel = new accountViewModel();
    ko.applyBindings(viewmodel);
});