using ASPNETCOREDEMO.Data;
using ASPNETCOREDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ASPNETCOREDEMO.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplictionDbContext _db;

        public CategoryController(ApplictionDbContext db)

        {
            _db = db;
        }
        public IActionResult Index1(string searching,  int pg=1)
        {
            IEnumerable<Category> objlist = _db.categories;

            if (searching != null)
            {
             
                return View(_db.categories.Where(x => x.Name.Contains(searching) || searching == null).ToList());

            }

            const int pageSize = 2;
            if(pg < 1)
                pg = 1;

            int recsCount = objlist.Count();
            var pager = new Pager(recsCount, pg, pageSize);


            int recSkip = (pg - 1) * pageSize;

            var data = objlist.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;


            return View(data);
        }

        //GET-CREATE
        public IActionResult Create()
        {
         
            return View();
        }

        //Post-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index1");

            }
           
            return View(obj);
        }


        //GET-EDIT
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var obj=_db.categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index1");

            }

            return View(obj);
        }

        //GET-Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.categories.Find(id);

            if (obj == null)
            {
                return NotFound();

            }
                _db.categories.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index1");
            
            
            
           
        }


    }
}
