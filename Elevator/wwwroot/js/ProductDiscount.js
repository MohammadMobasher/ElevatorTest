﻿

$(function () {
    var productBoxes = $('.discountProduct');
    $.each(productBoxes, function (index, key) {
        var id = $(key).attr('data-id');
        $.get("/Product/CalculateDiscount", { id: id }, function (result) {
            if (result !== false) {
                
                console.log(result);
                $(key).append('<li class="flag-discount">' + result.item2 + "  " + (result.item3 == 1 ? "% تخفیف" : "تومان ") + '</li>');
                $('.dicountValue_' + id).addClass("discount");
                $('.dicountValue_' + id).html(result.item2 + "  " + (result.item3 == 1 ? "% تخفیف" : "تومان "));
                $('.DiscountPrice_' + id).html(result.item1 + " تومان ");
                $('.countdown_Id_' + id).html('<div class="hoproduct-countdown countdown" data-countdown="' + result.item4 + '"></div>');
            }
            else {
                //$('.olPrice_' + id).css('display','none');
            }
        })

    })

})

$(function () {
    $('.VisitCounter').on('click', function () {
        var id = $(this).attr('data-id');
        $.get("/Product/AddVisit", { id: id }, function () { });
    })
})