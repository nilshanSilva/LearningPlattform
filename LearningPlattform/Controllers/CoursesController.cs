using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LearningPlattform.Models;
using LearningPlattform.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LearningPlattform.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public CoursesController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)); //for accessing current user
        }

        // GET: Courses
        public ActionResult Index()
        {
            ViewBag.CurrentUser = UserManager.FindById(User.Identity.GetUserId());
            return View(db.Courses.ToList());
        }
            
        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            List<Video> videos = db.Videos.Where(d => d.Course.Id == id).ToList();
            VideoCourseViewModel VCVM = new VideoCourseViewModel() { Videos = videos, Course = course };
            if (course == null)
            {
                return HttpNotFound();
            }
            var CurrentUser = UserManager.FindById(User.Identity.GetUserId());
            var EnrolledUsers = (from m in db.Courses
                                 from b in m.Users
                                 where b.Id == CurrentUser.Id
                                 where m.Id == course.Id
                                 select new { EnrollmentId = m.Id }).SingleOrDefault();
            if(EnrolledUsers != null)
            {
                ViewBag.Enrolled = true;
            }
            else
            {
                ViewBag.Enrolled = false;
            }
            var Instructor = UserManager.FindById(course.InstructorId);
            ViewBag.Instructor = Instructor;
            ViewBag.CurrentUser = CurrentUser;        
            return View(VCVM);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Instructor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost, Authorize(Roles = "Instructor")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Category,Language,CourseLevel,Price,InstructorId")] Course course, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var fileName = System.IO.Path.GetFileName(file.FileName);
                string fileType = fileName.Substring(fileName.LastIndexOf("."));

                if (file != null && file.ContentLength > 0 && (fileType == ".jpg" ||
                                         fileType == ".jpeg" || fileType == ".png"))
                {
                    var Name = Guid.NewGuid().ToString() + fileType;
                    var path = System.IO.Path.Combine(Server.MapPath("~/Uploads/Images"), Name);
                    string dbpath = "../Uploads/Images/" + Name.ToString();
                    file.SaveAs(path);
                    course.ImagePath = dbpath;
                }
                course.InstructorId = User.Identity.GetUserId().ToString();
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Instructor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [Authorize(Roles = "Instructor")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Category,Language,CourseLevel,Price")] Course course, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var fileName = System.IO.Path.GetFileName(file.FileName);
                string fileType = fileName.Substring(fileName.LastIndexOf("."));

                if (file != null && file.ContentLength > 0 && (fileType == ".jpg" ||
                                         fileType == ".jpeg" || fileType == ".png"))
                {
                    var Name = Guid.NewGuid().ToString() + fileType;
                    var path = System.IO.Path.Combine(Server.MapPath("~/Uploads/Images"), Name);
                    string dbpath = "../Uploads/Images/" + Name.ToString();
                    file.SaveAs(path);
                    course.ImagePath = dbpath;
                }

                    db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Instructor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [Authorize(Roles = "Instructor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Enroll(int Id)
        {
            var CurrentUser = UserManager.FindById(User.Identity.GetUserId());
            var user = db.Users.Find(User.Identity.GetUserId());
            var course = db.Courses.Include(c => c.Users).FirstOrDefault(d => d.Id == Id);
            course.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Details", "Courses", new { Id = course.Id });
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
