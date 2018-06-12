$(function () {
    $("#btnLog").click(function () {
        window.location.href = "/Account/Login";
    });

    $(".iframe").click(function () {
        window.location.href = $(this).data("link");
    });

    $("#btnSub").click(function () {
        window.location.href = "/Home/subscription";
    });
});