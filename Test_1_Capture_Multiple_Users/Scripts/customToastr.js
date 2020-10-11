var customToastr = {

    getToastrOptions: function () {
        var options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": true,
            "progressBar": false,
            "positionClass": "toast-top-full-width",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "12000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        return options;
    },

    OnValidationOrException: function (message) {
        toastr.options = customToastr.getToastrOptions();
        toastr.error(message)
    },

    OnSuccess: function (message) {
        toastr.options = customToastr.getToastrOptions();
        toastr.success(message)
    }
}

