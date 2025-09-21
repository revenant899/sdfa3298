﻿using sdfa3298.ViewModels;
using System.Text.Json;

namespace sdfa3298.Services
{
    public static class CartService
    {
        public static void AddToCart(this ISession session, int productId)
        {
            if (!session.ItemInCart(productId))
            {
                var items = session.CartItems();
                items.Add(new CartItemVM { ProductId = productId });
                session.SetCartItems(items);
            }
        }

        public static void RemoveFromCart(this ISession session, int productId)
        {
            if (session.ItemInCart(productId))
            {
                var items = session.CartItems();
                items = items.Where(i => i.ProductId != productId).ToList();
                session.SetCartItems(items);
            }
        }

        public static void SetCartItems(this ISession session, List<CartItemVM> items)
        {
            string json = JsonSerializer.Serialize(items);
            session.SetString(Settings.CartKey, json);
        }

        public static List<CartItemVM> CartItems(this ISession session)
        {
            var json = session.GetString(Settings.CartKey);
            if (json == null)
            {
                return new List<CartItemVM>();
            }

            var items = JsonSerializer.Deserialize<List<CartItemVM>>(json);
            return items == null ? new List<CartItemVM>() : items;
        }

        public static int GetCartCount(this ISession session)
        {
            var items = session.CartItems();
            return items.Count;
        }

        public static bool ItemInCart(this ISession session, int productId)
        {
            var items = session.CartItems();
            return items.Any(i => i.ProductId == productId);
        }

        public static void IncreaseCount(this ISession session, int productId, int maxValue)
        {
            var items = session.CartItems();
            var item = items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                if (item.Count < maxValue)
                {
                    item.Count++;
                    session.SetCartItems(items);
                }
            }
        }

        public static void ClearCart(this ISession session)
        {
            session.SetCartItems(new List<CartItemVM>());
        }

        public static void DecreaseCount(this ISession session, int productId, int minValue)
        {
            var items = session.CartItems();
            var item = items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                if (item.Count > minValue)
                {
                    item.Count--;
                    session.SetCartItems(items);
                }
            }
        }
    }
}
