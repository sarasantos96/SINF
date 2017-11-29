function validateFormOnSubmit(theForm) {
    var utilizadorModel = {
        Username: $("#username").val(),
        Pass: $("#pass").val(),
        Email: $("#email").val()
    };
    console.log(utilizadorModel);
    $.ajax({
        type: "POST",
        url: 'http://localhost:49822/Clientes/CreateUtilizador',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(utilizadorModel),

        dataType: "json",
        success: function () { location.href = "http://localhost:49822/Clientes/LogIn" },
        error: function () { alert('An error occurred, please try again.'); }
    });
    return false;
}