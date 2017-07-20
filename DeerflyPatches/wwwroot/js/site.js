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
        success: function() {
            alert("Success!");          
        }
    });
}

