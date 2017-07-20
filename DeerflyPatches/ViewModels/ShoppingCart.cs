using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeerflyPatches.Models;

namespace DeerflyPatches.ViewModels
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

        public List<OrderDetail> GetItems()
        {
            return _shoppingCart;
        }

    }
}
