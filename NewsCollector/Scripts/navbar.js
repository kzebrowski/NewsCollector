$(function () {
    $("#btnLog").click(function () {
        window.location.href = "/Account/Login";
    });

    $("#btnAcc").click(function () {
        window.location.href = "/User/MyAccount";
    });

    $(".iframe").click(function () {
        window.location.href = $(this).data("link");
    });
});