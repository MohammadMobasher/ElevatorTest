function reload(href) {
    if (href != undefined)
        window.location = href;
    else
        location.reload();

}

// این قسمت مربوط به جستجو در قسمت جداول هست
$(function () {

    $('.searchInput').on("keypress", function (event) {

        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $('.searchBtn').trigger('click');
        }

    });



});


// این قسمت مربوط به زمانی است که در داخل یک جدول یک سطر انتخاب می‌شود
// و باید در ادامه این کارهایی انجام شود
$(function () {
    // برای زمانی که دکمه برگشت زده می‌شود
    $("input[data-role-table-checkbox]").prop("checked", false);

    $("input[data-role-table-checkbox]").click(function () {
        if ($(this).is(':checked')) {
            $("input[data-role-table-checkbox]").parent().parent().removeClass("tr-selected");
            $(this).parent().parent().addClass("tr-selected");

            $("span[data-role-table-btn-area-access]").removeClass("hidden");

            $("input[data-role-table-checkbox]").removeAttr("checked");
            $(this).prop("checked", true);

            $("[name='selectedRowInTable']").val($(this).attr('data-role-table-checkbox'));

        }
        else {
            $(this).parent().parent().removeClass("tr-selected");

            $("span[data-role-table-btn-area-access]").addClass("hidden");

            $("[name='selectedRowInTable']").val("");
        }
    });


});


// این قسمت مربوط به بخش دکمه های بالا جداول است
// وقتی روی دکمه ایی زد، شماره آیتم انتخاب شده به انتحای آدرسی که در دکمه قرار دارد
// اضافه میکند و سپس به سمت آن صفحه می‌رود
$(function () {
    $(".data-role-table-btn").click(function () {


        var attr = $(this).attr('ismodal');

        var href = $(this).attr("data-role-href") + "/";

        
        if ($("[name='selectedRowInTable']").val() != "")
            href += $("[name='selectedRowInTable']").val();


        // باز شدن در یک مودال
        if ((typeof attr !== typeof undefined && attr !== false)) {
            CustomOpenModal(href, $(this).attr('modal-title'));
        }
        // باز شدن به صورت عادی و در همان صفحه
        else {
            reload(href);
        }

    });
});



//  ajax
// به صورت معمولی
function AjaxCall(url, params, type, successCallback) {


    // نمایش لودینگ
    $('.loading').removeClass("hide");

    timeOut = 6000;

    if (type == null)
        type = "POST";

    type = (type.toUpperCase() != "GET" &&
        type.toUpperCase() != "POST" &&
        type.toUpperCase() != "PUT" &&
        type.toUpperCase() != "DELETE") ? "POST" : type;


    $.ajax({
        type: type,
        url: url,
        async: false,
        data: params,
        success: successCallback,
        error: function (xhr, textStatus, errorThrown) {
            console.log(xhr.status);
            // درصورتی که کوکی شخص پریده باشد باید به صفحه لاگین هدایت شود
            // درصورتی که شماره status برابر 401 باشد باید به صفحه لاگین هدایت شود
            if (xhr.status == 401)
                window.location.href = "/";

            // درصورتی که کاربر به این اکشن دسترسی نداشته باشد
            if (xhr.status == 403) {
                $("[name='NoAccess']").removeClass("hide");
                $('#target_').html('');
            }
        }
    });

    // پنهان کردن لودینگ
    $('.loading').addClass("hide");
}


function AjaxCall(url, params, type) {

    
    // نمایش لودینگ
    $('.loading').removeClass("hide");

    timeOut = 6000;
    var returnData ;
    if (type == null)
        type = "POST";

    type = (type.toUpperCase() != "GET" &&
        type.toUpperCase() != "POST" &&
        type.toUpperCase() != "PUT" &&
        type.toUpperCase() != "DELETE") ? "POST" : type;


    $.ajax({
        type: type,
        url: url,
        async: false,
        data: params,
        success: function (data) {
            console.log("mohammad");
            // پنهان کردن لودینگ
            $('.loading').addClass("hide");
            
            returnData = data;
            
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log("mohammad1");
            console.log(xhr.status);
            // درصورتی که کوکی شخص پریده باشد باید به صفحه لاگین هدایت شود
            // درصورتی که شماره status برابر 401 باشد باید به صفحه لاگین هدایت شود
            if (xhr.status == 401)
                window.location.href = "/";

            // درصورتی که کاربر به این اکشن دسترسی نداشته باشد
            if (xhr.status == 403) {
                $("[name='NoAccess']").removeClass("hide");
                $('#target_').html('');
            }
        }
    });

    // پنهان کردن لودینگ
    $('.loading').addClass("hide");
    return returnData;
}

//function AjaxCallWithCondistion(url, params, type, successCallback) {

//    // نمایش لودینگ
//    $('.loading').removeClass('hide');

//    timeOut = 6000;

//    if (type == null)
//        type = "POST";

//    type = (type.toUpperCase() != "GET" &&
//        type.toUpperCase() != "POST" &&
//        type.toUpperCase() != "PUT" &&
//        type.toUpperCase() != "DELETE") ? "POST" : type;


//    $.ajax({
//        type: type,
//        url: url,
//        async: false,
//        data: params,
//        processData: false,
//        contentType: false,
//        success: successCallback,
//        error: function (xhr, textStatus, errorThrown) {
//            console.log(xhr.status);
//            // درصورتی که کوکی شخص پریده باشد باید به صفحه لاگین هدایت شود
//            // درصورتی که شماره status برابر 401 باشد باید به صفحه لاگین هدایت شود
//            if (xhr.status == 401)
//                window.location.href = "/";

//            if (xhr.status == 403) {
//                $("[name='NoAccess']").removeClass("hide");
//                $('#target_').html('');
//            }
//        }
//    });
    
//    //پنهان کردن لودینگ
//    $('.loading').addClass('hide');
//}

/*
 * از این تابع کلا برای زمانی که بخواهیم کلا
 * یا فرمو به سمت سرور بفرستیم و یا کلا در داخل صفحه فایل آپلود داشته باشیم استفاده می کنیم
 */
function AjaxCallWithUploadFile(url, params, type, successCallback) {
    // نمایش لودینگ
    $('.loading').removeClass('hide');

    timeOut = 6000;

    if (type == null)
        type = "POST";

    type = (type.toUpperCase() != "GET" &&
        type.toUpperCase() != "POST" &&
        type.toUpperCase() != "PUT" &&
        type.toUpperCase() != "DELETE") ? "POST" : type;


    $.ajax({
        type: type,
        url: url,
        async: false,
        data: params,
        cache: false,
        contentType: false,
        processData: false,

        success: successCallback,
        error: function (xhr, textStatus, errorThrown) {
            console.log(xhr.status);
            // درصورتی که کوکی شخص پریده باشد باید به صفحه لاگین هدایت شود
            // درصورتی که شماره status برابر 401 باشد باید به صفحه لاگین هدایت شود
            if (xhr.status == 401)
                window.location.href = "/";

            if (xhr.status == 403) {
                $("[name='NoAccess']").removeClass("hide");
                $('#target_').html('');
            }
        }
    });


    
    // پنهان کردن لودینگ
    $('.loading').addClass('hide');
}


// از این تابع باز کردن مودال و قرار دادن یک صفحه در آن استفاده می شود
// برای استفاده از این تابع باید ابتدا آدرس صفحه ای که باید لود شود داده شده
// تا با یک ajax
// صفحه مورد نظر آورده شده و نمایش داده شود
// همچنین عنوان صفحه نیز باید داده شود
function CustomOpenModal(url, modalTitle) {
    
    $('[name="LiftBazarModalContent"]').html(AjaxCall(url, null, "GET"));

    $('[name="LiftBazarModalTitle"]').html(modalTitle); 
    
    $('#LiftBazarModal').modal('show');

}




$(".btn").click(function () {



});