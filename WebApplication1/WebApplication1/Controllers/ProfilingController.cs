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
    public class ProfilingController : Controller
    {
        private readonly MyContext myContext;

        public ProfilingController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            var profilings = myContext.Profilings
                .Include(e => e.Account).Include(a => a.Education)
                .ToList();

            return View(profilings);
        }
        public IActionResult Create()
        {
            List<Education> educationList = new List<Education>();
            educationList = (from product in myContext.Educations select product).ToList();

            educationList.Insert(0, new Education { Id = 0, Degree = "Select" });
            ViewBag.ListofEducation = educationList;

            List<Account> accountList = new List<Account>();
            accountList = (from product in myContext.Accounts select product).ToList();
            
            accountList.Insert(0, new Account { NIK="", FirstName ="Select"});
            ViewBag.ListofAccount = accountList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Profiling profiling)
        {
            if (profiling.NIK == "" || profiling.Education_Id == 0 )
                ModelState.AddModelError("", "You must Select");
            string SelectedValue = profiling.NIK;
            myContext.Profilings.Add(profiling);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index");
            return View();
        }
        public IActionResult Edit(string NIK)
        {
            if (NIK == "")
            {
                return View();
            }

            var result = myContext.Profilings.SingleOrDefault(x => x.NIK == NIK);

            List<Education> educationList = new List<Education>();
            educationList = (from product in myContext.Educations select product).ToList();

            educationList.Insert(0, new Education { Id = 0, Degree = "Select" });
            ViewBag.ListofEducation = educationList;


            if (result == null)
                return View();
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string NIK, Profiling profiling)
        {
            if (NIK == "")
            {
                return View();
            }
            var get = myContext.Profilings.Find(NIK);
            if (get != null)
            {
                get.Education_Id = profiling.Education_Id;
                myContext.Entry(get).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                return View();
            }
            return View();
        }

        public ActionResult Delete(string NIK)
        {
            var get = myContext.Profilings.Find(NIK);
            if (get != null)
            {
                myContext.Profilings.Remove(get);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Json(result);

            }
            return Json(NIK);
        }
    }
}
