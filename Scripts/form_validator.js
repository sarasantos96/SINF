﻿function validateFormOnSubmit(theForm) {
    var utilizadorModel = {
        Username: $("#username").val(),
        Pass: $("#pass").val(),
        Email: $("#email").val(),
        Fullname: $("#fullname").val()
    };

    $.ajax({
        type: "POST",
        url: 'http://localhost:49822/Clientes/CreateUtilizador',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(utilizadorModel),

        dataType: "json",
        success: function (data) {
             if(data != null && data.success) {
                location.href = "http://localhost:49822/Clientes/LogIn"
            }  else {
                alert('Username already taken, please try again!');
            }
        },
        error: function () {
            alert('An error occurred, please try again!');
        }
    });
    return false;
}

function validateLogInOnSubmit(theForm) {
    var utilizadorModel = {
        Username: $("#username").val(),
        Pass: $("#pass").val(),
    };
    console.log(utilizadorModel);
    $.ajax({
        type: "POST",
        url: 'http://localhost:49822/Clientes/CheckLogIn',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(utilizadorModel),
        dataType: 'json',
        success: function (data) {
            if (data != null && data.success && data.msg == "admin") {
                location.href = "http://localhost:49822/Admin"
            } else if (data != null && data.success) {
                location.href = "http://localhost:49822"
            } else {
                alert('Incorrect Credentials, try again!');
            }
        },
        error: function () {
            alert('Incorrect Credentials, try again!');
        }
    });
    return false;
}