using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeerflyPatches.Models
{
    public class ShoppingCart
    {
        public List<OrderDetail> _shoppingCart { get; set; }

        public ShoppingCart()
        {
            _shoppingCart = new List<OrderDetail>();
        }

        public void Add(OrderDetail newItem)
        {
            _shoppingCart.Add(newItem);
        }

    }
}
