$(function () {
    $(window).load(function () {
        $.get("/Cart/CountItem", function (data) {
            $("#cart").html(data);
        });
    })
});