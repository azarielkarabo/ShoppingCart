
function contactVM() {
    var self = this;

    self.firstName = ko.observable();
    self.lastName = ko.observable();
    self.email = ko.observable();
    self.cellNumber = ko.observable();
    self.message = ko.observable();

    self.send = function () {

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
            }
        });

    };

    self.clean = function () {
        self.firstName(null);
        self.lastName(null);
        self.email(null);
        self.cellNumber(null);
        self.message(null);
    };
}


$(document).ready(function () {
    var viewmodel = new contactVM();
    ko.applyBindings(viewmodel);
})