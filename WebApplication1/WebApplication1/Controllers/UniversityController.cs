using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UniversityController : Controller
    {
        private readonly MyContext myContext;

        public UniversityController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            var universities = myContext.Universities
                .Where(x => x.IsAvailable == true)
                .OrderBy(x => x.Name)
                .ToList();
            return View(universities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(University university)
        {
            myContext.Universities.Add(university);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index");
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return View();
            }
            var result = myContext.Universities.SingleOrDefault(x => x.Id == id);
            if (result == null)
                return View();
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(int? id, University university)
        {
            if (id == null)
            {
                return View();
            }
            var get = myContext.Universities.Find(id);
            if (get != null)
            {
                get.Name = university.Name;
                get.IsAvailable = university.IsAvailable;
                myContext.Entry(get).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                return View();
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var get = myContext.Universities.Find(id);
            if(get != null)
            {
                myContext.Universities.Remove(get); 
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Json(result);

            }
            return Json(id);
        }
    }
}
