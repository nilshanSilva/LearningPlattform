﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LearningPlattform.Models;
using LearningPlattform.Models.ViewModels;
using System.Data.Entity.Validation;
using System;

namespace LearningPlattform.Controllers
{
    public class VideosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Videos
        public ActionResult Index()
        {
            return View(db.Videos.ToList());
        }

        // GET: Videos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // GET: Videos/Create
        [Authorize(Roles = "Instructor")]
        public ActionResult Create(int CourseId)
        {
            Course course = db.Courses.Find(CourseId);
            SingleVideoCourseVM VCVM = new SingleVideoCourseVM() { Course = course };
            return View(VCVM);
        }

        // POST: Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        ///[HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int CourseId, [Bind(Include = "Id,Name,Description,Path")] Video video, HttpPostedFileBase file)
         {
            if (ModelState.IsValid)
            {
                Course course = db.Courses.Find(CourseId);
                video.Course = course;

                var fileName = System.IO.Path.GetFileName(file.FileName);
                //string fileType = fileName.Substring(fileName.LastIndexOf("."));

                if (file != null && file.ContentLength > 0 /*&& fileType == ".mp4"*/)
                {

                    var guid = Guid.NewGuid().ToString();
                    var path = System.IO.Path.Combine(Server.MapPath("~/Uploads/Videos"), guid + fileName);
                    string dbpath = "../Uploads/Videos/" + guid.ToString() + fileName;
                    file.SaveAs(path);
                    video.Path = dbpath;
                }

                db.Videos.Add(video);
                db.SaveChanges();
                 return RedirectToAction("Details", "Courses", new { Id = video.Course.Id });
              //  return View("Details", "Course")
               // return RedirectToAction("Index");
               //developer.paypal.com
            }

            return RedirectToAction("Index");
        }

        // GET: Videos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Path")] Video video)
        {
            if (ModelState.IsValid)
            {
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(video);
        }

        // GET: Videos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = db.Videos.Find(id);
            db.Videos.Remove(video);
            db.SaveChanges();
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
