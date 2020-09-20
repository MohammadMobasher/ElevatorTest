$(function () {
    var elements = document.getElementsByTagName("INPUT");
    for (var i = 0; i < elements.length; i++) {
        elements[i].oninvalid = function (e) {
            e.target.setCustomValidity("");
            if (!e.target.validity.valid) {
                e.target.setCustomValidity("این فیلد نمی تواند خالی باشد");
            }
        };
        elements[i].oninput = function (e) {
            e.target.setCustomValidity("");
        };
    }

    var select = document.getElementsByTagName("SELECT");
    for (var i = 0; i < select.length; i++) {
        select[i].oninvalid = function (e) {
            e.target.setCustomValidity("");
            if (!e.target.validity.valid) {
                e.target.setCustomValidity("این فیلد نمی تواند خالی باشد");
            }
        };
        select[i].oninput = function (e) {
            e.target.setCustomValidity("");
        };
    }
});

function generatePDF(id) {
    // Choose the element that our invoice is rendered in.
    const element = document.getElementById(id);
    // Choose the element and save the PDF for our user.
    html2pdf()
        .from(element)
        .save();
}


