// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function CallAsync(metodo, vista, destino) {
    $.blockUI({
        message: '<p><i class="fas fa-sync fa-spin me-2"></i> Espera un momento...</p>',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });
    $.ajax({
        type: metodo,
        url: vista,
        dataType: "html",
        success: function (response) {
            $(destino).html(response);
            $.unblockUI();
        },
        failure: function (response) {
            alertify.error(response.responseText);
            $.unblockUI();
        },
        error: function (response) {
            alertify.error(response.responseText);
            $.unblockUI();
        }
    });
}
function setActualDate(inputDate) {
    var now = new Date();

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);

    $(inputDate).val(today);
}