using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core_WebApp.Models;
using Core_WebApp.Services;
namespace Core_WebApp.Controllers
{
    /// <summary>
    /// DI the Repository Classes
    /// Controller class is a base class for MVC 
    /// </summary>
    public class CategoryController : Controller
    {
        private readonly IRepository<Category, int> repository;
        public CategoryController(IRepository<Category, int> repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var res = await this.repository.GetAsync();
            return View(res);
        }

        public IActionResult Create()
        {
            return View(new Category());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var res = await repository.GetAsync(id);
            return View(res);//return view with data to be edited
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await repository.DeleteAsync(id);
            if(res)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index"); //Stay on the same Index view
        }


        [HttpPost]
        public async Task<IActionResult> Create(Category c)
        {
            try
            {
                //check for the validation
                if (ModelState.IsValid)
                {
                    await repository.CreateAsync(c);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");//Stay on "Create view" with validation error messages

            }
            catch (Exception)
            {
                throw;
            }        
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category c)
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