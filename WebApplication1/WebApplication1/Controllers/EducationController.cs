using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EducationController : Controller
    {
        private readonly MyContext myContext;

        public EducationController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        // GET: EducationController
        public ActionResult Index()
        {
            var educations = myContext.Educations
                .Include(e => e.University)
                .ToList();

            return View(educations);
        }


        // GET: EducationController/Create
        public ActionResult Create()
        {
            List<University> universitiesList = new List<University>();
            universitiesList = (from product in myContext.Universities select product).ToList();

            universitiesList.Insert(0, new University { Id = 0, Name = "Select" });
            ViewBag.ListofUniversity = universitiesList;
            return View();
        }

        // POST: EducationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Education education)
        {
            if (education.University_Id == 0)
                ModelState.AddModelError("", "Select University");
            int SelectedValue = education.University_Id;

            education.University_Id = SelectedValue;

            myContext.Educations.Add(education);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index");
            return View();
        }

        // GET: EducationController/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var result = myContext.Educations.SingleOrDefault(x => x.Id == id);

            List<University> universitiesList = new List<University>();
            universitiesList = (from product in myContext.Universities select product)
                .ToList();

            universitiesList.Insert(0, new University { Id = 0, Name = "Select" });
            ViewBag.ListofUniversity = universitiesList;

            if (result == null)
                return View();
            return View(result);
        }

        // POST: EducationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Education education)
        {
            if (id == null)
            {
                return View();
            }
            var get = myContext.Educations.Find(id);
            if (get != null)
            {
                get.Degree = education.Degree;
                get.GPA = education.GPA;
                get.University_Id = education.University_Id;
                myContext.Entry(get).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                return View();
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            var get = myContext.Educations.Find(id);
            if (get != null)
            {
                myContext.Educations.Remove(get);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Json(result);

            }
            return Json(id);
        }
    }
}
