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
            alert('Success!');          
        },
        error: function (returnval) {
            alert('Error adding item to shopping cart :( ');
        }
    });
}

function incrementItemInShoppingCart(id) {
    var postData = {
        __RequestVerificationToken: antiForgeryToken(),
        ID: id
    };
    $.post({
        url: '/OrderDetails/AddOrderDetailToShoppingCart/',
        data: postData,
        dataType: 'json',
        success: function (returnval) {
            var $qty = $('#qty-' + id)[0];
            $qty.innerText = parseInt($qty.innerText) + 1;
        },
        error: function (returnval) {
            alert('Error incrementing item in shopping cart :( ');
        }
    });
}


function decrementItemInShoppingCart(id) {
    var postData = {
        __RequestVerificationToken: antiForgeryToken(),
        ID: id
    };
    var $qty = $('#qty-' + id)[0];
    if ($qty.innerText < 1) {
        alert("No items to remove!");
        return;
    }
    $.post({
        url: '/OrderDetails/DecrementItemInShoppingCart/',
        data: postData,
        dataType: 'json',
        success: function (returnval) {
            $qty.innerText = parseInt($qty.innerText) - 1;
        },
        error: function (returnval) {
            alert("Error decrementing item in shopping cart :( ");
        }
    });
}

function removeProductInShoppingCart(id) {
    var postData = {
        __RequestVerificationToken: antiForgeryToken(),
        ID: id
    };
    $.post({
        url: '/OrderDetails/RemoveProductInShoppingCart/',
        data: postData,
        dataType: 'json',
        success: function (returnval) {
            var $item = $('#item-' + id)[0];
            $item.remove();
        },
        error: function (returnval) {
            alert("Error removing item from shopping cart :( ");
        }
    });
}