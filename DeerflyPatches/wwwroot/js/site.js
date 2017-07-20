function antiForgeryToken() {
    return $('#anti-forgery-token input')[0].value;
}


function addToShoppingCart(id) {
    var postData = {
        __RequestVerificationToken: antiForgeryToken(),
        ID: id
    };
    $.post({
        url: '/OrderDetails/AddOrderDetailToShoppingCart/',
        data: postData,
        dataType: 'json',
        success: function(returnval) {
            alert("Success!");          
        },
        error: function (returnval) {
            debugger;
            alert("Error adding item to shopping cart :( ");
        }
    });
}

