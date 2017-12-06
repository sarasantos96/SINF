function validateFormOnSubmit(theForm) {
    var utilizadorModel = {
        Username: $("#username").val(),
        Pass: $("#pass").val(),
        Email: $("#email").val(),
        Fullname: $("#fullname").val(),
        TaxPayerNumber: $("#taxPayerNumber").val(),
        Address: $("#address").val()
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

function addProductToCart(theForm) {
    var quantity = 1;
    if ($("#quantity").val() > 0) {
        quantity = $("#quantity").val();
    }
    var artigoModel = {
        CodArtigo: $("#codArtigo").val(),
        DescArtigo:$('#descArtigo').val(), 
        Preco: $("#precoArtigo").val(),
        Quantidade : quantity
    };
    $.ajax({
        type: "POST",
        url: 'http://localhost:49822/Artigos/AdicionaArtigoCarrinho',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(artigoModel),
        dataType: 'json',
        success: function (data) {
            if (data != null && data.success) {
                alert("Product added to Cart!");
            } else {
                alert('An error occurred, please try again!');
            }
        },
        error: function () {
            alert('An error occurred, please try again!');
        }
    });
    return false;
}

function removeProduct(id) {
    var j = {
        id: id
    };
    $.ajax({
        type: "POST",
        url: 'http://localhost:49822/ShoppingCart/RemoveArtigo',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(j),
        dataType: 'json',
        success: function (data) {
            if (data != null && data.success) {
                location.href = "http://localhost:49822/ShoppingCart"
            } else {
                alert('An error occurred, please try again!');
            }
        },
        error: function () {
            alert('An error occurred, please try again!');
        }
    });
    return false;
}

function createPriOrder(username, address, name, taxpayernumber) {
    $('#CancelBtn').attr('disabled', 'disabled');
    $('#CreateOrderBtn').attr('disabled', 'disabled');
    var j = {
        CodCliente: username,
        NomeCliente: name,
        NumContribuinte: taxpayernumber,
        Morada: address
    };
    $.ajax({
        type: "POST",
        url: 'http://localhost:49822/DocVenda/Post',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(j),
        dataType: 'json',
        success: function (data) {
            if (data != null && data.success) {
                location.href = "http://localhost:49822/ShoppingCart";
                alert("Order created");
            } else {
                alert('An error occurred, please try again!');
            }
        },
        error: function () {
            alert('An error occurred, please try again!');
        }
    });
    return false;
}

function updateUser() {
    var j = {
        Email: $("#email").val(),
        Fullname: $("#fullname").val(),
        TaxPayerNumber: $("#taxPayerNumber").val(),
        Address: $("#address").val()
    };
    $.ajax({
        type: "POST",
        url: 'http://localhost:49822/Clientes/Put',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(j),
        dataType: 'json',
        success: function (data) {
            if (data != null && data.success) {
                location.href = "http://localhost:49822/Clientes/Cliente";
            } else {
                alert('An error occurred, please try again!');
            }
        },
        error: function () {
            alert('An error occurred, please try again!');
        }
    });
    return false;
}

function updatePassword() {
    var j = {
        oldPass: $("#oldpass").val(),
        newPass: $("#newpass").val(),
    };
    $.ajax({
        type: "POST",
        url: 'http://localhost:49822/Clientes/UpdatePassword',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(j),
        dataType: 'json',
        success: function (data) {
            if (data != null && data.success) {
                location.href = "http://localhost:49822/Clientes/Cliente";
            } else {
                alert('An error occurred, please try again!');
            }
        },
        error: function () {
            alert('An error occurred, please try again!');
        }
    });
    return false;
}

function loadDefaultImage(obj) {

    document.getElementById(obj.getAttribute('id')).src = 'http://saveabandonedbabies.org/wp-content/uploads/2015/08/default.png';
}

function searchProducts(theForm) {
    var query = $("#searchbar").val();
    console.log(query);
    if (query != "") {
        location.href = "http://localhost:49822/Artigos/SearchResults?query=" + query;
    }
    
    return false;
}