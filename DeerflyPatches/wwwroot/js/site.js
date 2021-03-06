﻿function antiForgeryToken() {
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
            recalculate();
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

    var qty = parseInt($qty.innerText);
    if (qty < 1) {
        alert('No items to remove!');
        return;
    }
    if (qty === 1) {
        alert('Minimum quantity of 1');
        return;
    }

    $.post({
        url: '/OrderDetails/DecrementItemInShoppingCart/',
        data: postData,
        dataType: 'json',
        success: function (returnval) {
            $qty.innerText = parseInt($qty.innerText) - 1;
            recalculate();
        },
        error: function (returnval) {
            alert('Error decrementing item in shopping cart :( ');
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
            recalculate();
        },
        error: function (returnval) {
            alert('Error removing item from shopping cart :( ');
        }
    });
}

function recalculate() {
    var $itemDetailLines = $('.item-detail-line');
    var extendedPriceTotal = 0;
    var shippingTotal = 0;
    $itemDetailLines.each(function () {
        var linePrice = parseFloat($(this).find('.item-unit-price')[0].innerText.slice(1));
        var lineQty = parseInt($(this).find('.item-qty-ct')[0].innerText);

        var $itemExtendedPrice = $(this).find('.item-extended-price')[0];
        var $itemShipping = $(this).find('.item-shipping')[0];
        var $itemTotalPrice = $(this).find('.item-total-price')[0];

        var itemExtendedPrice = 1.0 * linePrice * lineQty;
        var itemShipping = parseFloat($itemShipping.innerText.slice(1));
        var itemTotalPrice = 1.0 * itemExtendedPrice + itemShipping;

        $itemExtendedPrice.innerHTML = '$' + itemExtendedPrice.toFixed(2);
        $itemShipping.innerHTML = '$' + itemShipping.toFixed(2);
        $itemTotalPrice.innerHTML = '$' + itemTotalPrice.toFixed(2);

        extendedPriceTotal += itemExtendedPrice;
        shippingTotal += itemShipping;
    });

    $('.item-detail-total .item-extended-price')[0].innerText = '$' + extendedPriceTotal.toFixed(2);
    $('.item-detail-total .item-shipping')[0].innerText = '$' + shippingTotal.toFixed(2);
    $('.item-detail-total .item-total-price')[0].innerText = '$' + (extendedPriceTotal + shippingTotal).toFixed(2);
}


function updateShippingAddress() {
    var formdata = $('#shipping-address').serializeArray();
    $.post({
        url: '/Addresses/UpdateShippingAddress',
        data: formdata,
        dataType: 'json',
        success: function(result, data) {
            
        },
        error: function(result) {
            debugger;
        }
    });
}