using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core_WebApp.Models;
using Core_WebApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core_WebApp.Controllers
{
    public class ProductController : Controller
    { 



        private readonly IRepository<Product, int> repository;
        private readonly IRepository<Category, int> catRepository;
        public ProductController(IRepository<Product, int> repository, IRepository<Category, int> catRepository)
        {
            this.repository = repository;
            this.catRepository = catRepository;
        }

        public async Task<IActionResult> Index()
        {
            var res = await this.repository.GetAsync();
            foreach (Product p in res.ToList())
            {
                p.Category = await catRepository.GetAsync(p.CategoryRowId);
            }
            return View(res);
        }

        public async Task<IActionResult> Create()
        {
            var res = new Product();
            ViewBag.CategoryRowId = new SelectList(await catRepository.GetAsync(), "CategoryRowId", "CategoryName");
            return View(res);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var res = await repository.GetAsync(id);
            return View(res);//return view with data to be edited
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await repository.DeleteAsync(id);
            if (res)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index"); //Stay on the same Index view
        }


        [HttpPost]
        public async Task<IActionResult> Create(Product c)
        {
            //check for the validation
            try
            {
                if (ModelState.IsValid)
                {
                    if(c.Price < 0)
                    {
                        throw new Exception("Product price cannot be negative");
                    }
                    await repository.CreateAsync(c);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.CategoryRowId = new SelectList(await catRepository.GetAsync(), "CategoryRowId", "CategoryName");
                    return View();//Stay on "Create view" with validation error messages
                }

            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel()
                {
                    ControllerName = this.RouteData.Values["controller"].ToString(),
                    ActionName = this.RouteData.Values["action"].ToString(),
                    ErrorMessage = ex.Message
                });
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product c)
        {
            if (ModelState.IsValid)
            {
                var res = await repository.UpadteAsync(id, c);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }





    }
}