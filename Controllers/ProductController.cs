using Microsoft.Ajax.Utilities;
using ProductManagement.Models;
using ProductManagement.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository productRepository;
        private readonly string connectionString;
        
        public ProductController()
        {
            connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductMgmt;Integrated Security=True;";
            productRepository = new ProductRepository(connectionString);
        }

        // GET: Products
        public ActionResult Index()
        {
            var products = productRepository.GetProducts();
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productRepository.AddProduct(product);
                    return RedirectToAction("Index");
                }

                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error occurred while adding a new product.";
                return View("Error");
            }
        }

        // GET: Products/Delete/1
        public ActionResult Delete(int id)
        {
            var product = productRepository.GetProductById(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/1
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            productRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }



        // GET: Products/Edit/1
        public ActionResult Edit(int id)
        {
            var product = productRepository.GetProductById(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }



        // POST: Products/Edit/1
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(Product updatedProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productRepository.UpdateProduct(updatedProduct);
                    return RedirectToAction("Index");
                }

                return View(updatedProduct);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ViewBag.ErrorMessage = "Error occurred while updating the product.";
                return View("Error");
            }
        }

    }
}
