﻿using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using sdfa3298.Models;
using sdfa3298.ViewModels;


namespace sdfa3298.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Models.Product> Products => _context.Products;

        public IQueryable<Models.Product> GetByCategory(string category, PaginationVM pagination)
        {
            var products = Products
                .Include(p => p.Category)
                .Where(p => p.Category!.Name.ToLower() == category.ToLower());
            products = SetPagination(products, pagination);
            return products;
        }

        public IQueryable<Models.Product> GetByPagination(PaginationVM pagination)
        {
            return SetPagination(Products, pagination);
        }

        public IQueryable<Models.Product> SetPagination(IQueryable<Models.Product> products, PaginationVM pagination)
        {
            pagination.TotalCount = products.Count();
            int pageCount = (int)Math.Ceiling((double)pagination.TotalCount / pagination.PageSize);
            pagination.PageCount = pageCount;

            int page = pagination.Page < 1 ? 1 : pagination.Page;
            page = page > pagination.PageCount ? 1 : page;
            pagination.Page = page;

            products = products.Skip(pagination.PageSize * (pagination.Page - 1)).Take(pagination.PageSize);

            return products;
        }
        public Models.Product Create(Models.Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }
        public Models.Product? Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return null;
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return product;
        }

        public Models.Product? GetById(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);
        }
        public Models.Product? GetByName(string name)
        {
            return _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
        }

        public Models.Product? Update(Models.Product product)
        {
            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            _context.SaveChanges();
            return existingProduct;
        }
        public IQueryable<Models.Product> GetByPrice(double min, double max, PaginationVM pagination)
        {
            var products = Products
                .Include(p => p.Category)
                .Where(p => p.Price >= min && p.Price <= max);
            products = SetPagination(products, pagination);
            return products;
        }
        public bool IsExist(string name)
        {
            return _context.Products.Any(p => p.Name.ToLower() == name.ToLower());
        }
    }
}
