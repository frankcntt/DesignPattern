$(function () {
    $("body").on("click", ".pop", function () {
        $('.imagepreview').attr('src', $(this).find('img').attr('src'));
        $('#imagemodal').modal('show');
    })
   
});