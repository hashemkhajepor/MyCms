using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyCms.Areas.Admin.Controllers
{
    public class News_GroupsController : Controller
    {
        UnitOfWork db = new UnitOfWork();

        // GET: Admin/News_Groups
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult ListGroup()
        {
            var news_Groups = db.NewsGroupsRepository.Get(g => g.ParentID == null);
            return PartialView(news_Groups.ToList());
        }
        // GET: Admin/News_Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News_Groups news_Groups = db.NewsGroupsRepository.GetById(id);
            if (news_Groups == null)
            {
                return HttpNotFound();
            }
            return View(news_Groups);
        }

        // GET: Admin/News_Groups/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/News_Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle,ParentID")] News_Groups news_Groups)
        {
            if (ModelState.IsValid)
            {
                db.NewsGroupsRepository.Insert(news_Groups);
                db.Save();
                return PartialView("ListGroup", db.NewsGroupsRepository.Get(g => g.ParentID == null));
            }

            ViewBag.ParentID = new SelectList(db.NewsGroupsRepository.Get(), "GroupID", "GroupTitle", news_Groups.ParentID);
            return View(news_Groups);
        }

        // GET: Admin/News_Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News_Groups news_Groups = db.NewsGroupsRepository.GetById(id);
            if (news_Groups == null)
            {
                return HttpNotFound();
            }
           
            return PartialView(news_Groups);
        }

        // POST: Admin/News_Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupTitle,ParentID")] News_Groups news_Groups)
        {
            if (ModelState.IsValid)
            {
                db.NewsGroupsRepository.Update(news_Groups);
                db.Save();
                return PartialView("ListGroup" , db.NewsGroupsRepository.Get(g=>g.ParentID == null));
            }
            ViewBag.ParentID = new SelectList(db.NewsGroupsRepository.Get(), "GroupID", "GroupTitle", news_Groups.ParentID);
            return View(news_Groups);
        }

        // GET: Admin/News_Groups/Delete/5
        public void Delete(int id)
        {
            var group = db.NewsGroupsRepository.GetById(id);
            if (group.News_Groups1.Any())
            {
                foreach(var item in db.NewsGroupsRepository.Get(g=>g.ParentID == id))
                {
                    db.NewsGroupsRepository.Delete(item);
                    foreach(var mingroup in db.NewsGroupsRepository.Get(d=>d.ParentID == item.GroupID))
                    {
                        db.NewsGroupsRepository.Delete(mingroup);
                    }
                }
            }
            db.NewsGroupsRepository.Delete(group);
            db.Save();
        }

        // POST: Admin/News_Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.NewsGroupsRepository.Delete(id);
            db.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
