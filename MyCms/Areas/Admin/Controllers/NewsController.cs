using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyCms.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        UnitOfWork db = new UnitOfWork();

        // GET: Admin/News
        public ActionResult Index()
        {
            return View(db.NewsRepository.Get());
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.NewsRepository.GetById(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            ViewBag.groups = db.NewsGroupsRepository.Get();
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsID,Title,ShortDescription,Text,ImageName,CreateDate")] News news , List<int> selectedGroups , HttpPostedFileBase files , string tags)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split(',');
                    foreach(var t in tag)
                    {
                        db.TagsRepository.Insert(new Tags()
                        {
                            NewsID = news.NewsID,
                            Tag = t.Trim()
                        });
                    }
                }
                if(selectedGroups == null)
                {
                    ViewBag.ErrorSelectedGroup = true;
                    ViewBag.groups = db.NewsGroupsRepository.Get();
                    return View(news);

                }
                foreach(int selectedGroup in selectedGroups)
                {
                    db.NewsSelectedGroupsRepository.Insert(new News_Selected_Groups()
                    {
                        GroupID = selectedGroup,
                        NewsID = news.NewsID
                    });
                }
                news.ImageName = "NoPhoto.jpg";
                if(files != null && files.IsImage())
                {
                    news.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(news.ImageName);
                    files.SaveAs(Server.MapPath("/Images/NewsImage/" + news.ImageName));
                    ImageResizer image = new ImageResizer();
                    image.Resize(Server.MapPath("/Images/NewsImage/" + news.ImageName),
                        Server.MapPath("/Images/NewsImage/Thumb/" + news.ImageName));
                }
                news.CreateDate = DateTime.Now;
                db.NewsRepository.Insert(news);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.NewsRepository.GetById(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsID,Title,ShortDescription,Text,ImageName,CreateDate")] News news)
        {
            if (ModelState.IsValid)
            {
                db.NewsRepository.Update(news);
                db.Save();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.NewsRepository.GetById(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.NewsRepository.Delete(id);
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
