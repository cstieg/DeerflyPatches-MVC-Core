(function renderPayPal() {
    paypal.Button.render({
        env: 'sandbox', // 'sandbox', 'production'

        client: {
            sandbox: 'AaLYEqNuR4sUTr6y7l7riLn6GhJDv6YY-1LrWwIyRHrRmXOlQjGscB8MQTUY_9unYy4SRSzhdVvRQ9_m',
            production: ''
        },

        commit: true, // Show a 'Pay Now' button

        payment: function (data, actions) {
            // Set up the payment here
            var grandTotal = parseFloat($('.item-detail-total .item-total-price')[0].innerText.slice(1));
            var totalExtendedPrice = parseFloat($('.item-detail-total .item-extended-price')[0].innerText.slice(1));
            var totalShipping = parseFloat($('.item-detail-total .item-shipping')[0].innerText.slice(1));

            var items = getTransactions();

            debugger;

            // Make AJAX call to check amount
            var shoppingCart;
            $.ajax({
                url: '/Orders/getShoppingCartJSON',
                success: function (shoppingCart) {
                    if (grandTotal !== shoppingCart.grandTotal) {
                        window.location = "/home/shoppingcart";
                        throw new Error("Grand total is incorrect!");
                    }
                }
            });

            return actions.payment.create({
                payment: {
                    intent: 'order',
                    transactions: [{
                        amount: {
                            total: grandTotal,
                            currency: 'USD',
                            details: {
                                subtotal: totalExtendedPrice,
                                shipping: totalShipping
                            },
                        },
                        description: 'Order from Detex',
                        item_list: items
                    }]
                }
            });
        },

        onAuthorize: function (data, actions) {
            // Execute the payment here

            // TODO: Check Country/ZIP!

            return actions.payment.get().then(function (paymentDetails) {

                // Show a confirmation using the details from paymentDetails
                // Then listen for a click on your confirm button

                debugger;

                document.querySelector('#confirm-button')
                    .addEventListener('click', function () {

                        // Execute the payment

                        return actions.payment.execute().then(function () {
                            // Show a success page to the buyer
                        });
                    });
            });
        }

    }, '#paypal-button');
})();


function getTransactions() {
    var items = [];

    var $itemDetailLines = $('.item-detail-line');
    $itemDetailLines.each(function (index, value) {
        var newItem = {};
        items.push(newItem);
        items[index].name = $(this).find('.item-name')[0].innerText;
        items[index].price = parseFloat($(this).find('.item-extended-price')[0].innerText.slice(1));
        items[index].currency = "USD";
        items[index].quantity = parseInt($(this).find('.item-qty-ct')[0].innerText);
    });

    return items;
}