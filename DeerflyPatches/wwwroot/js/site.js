// Write your Javascript code.
function addToShoppingCart(id) {
    debugger;
    var postData = {
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