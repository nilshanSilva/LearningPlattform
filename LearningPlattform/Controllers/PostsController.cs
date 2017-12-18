using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LearningPlattform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LearningPlattform.Models.ViewModels;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace LearningPlattform.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager { get; set; }
        public object ModelBindingHelper { get; private set; }

        public PostsController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Posts
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            List<Answer> answers = db.Answers.Include(c => c.Comments).Include(u => u.Answerer)
                .Include(l => l.Likes).Where(a => a.ParentPost.Id == id).Where(c => c.CorrectAnswer == false)
                .ToList();

            Answer correctAnswer = db.Answers.Include(c => c.Comments).Include(u => u.Answerer)
                .Include(l => l.Likes).Where(a => a.ParentPost.Id == id).Where(c => c.CorrectAnswer == true)
                .FirstOrDefault();

            

            ViewBag.Raiser = db.Users.Find(post.RaiserId);
            ViewBag.CurrentUser = db.Users.Find(User.Identity.GetUserId());
        

            PostViewModel PVM = new PostViewModel
            {
                Post = post,
                Answers = answers,
                CorrectAnswer = correctAnswer
            };

            if (post == null)
            {
                return HttpNotFound();
            }
            return View(PVM);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Question,Description")] Post post)
        {
            ModelState.Remove("RaiserId");
            if (ModelState.IsValid)
            {
                post.RaiserId = User.Identity.GetUserId().ToString();
                post.PostedDate = DateTime.UtcNow;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostAnswer([Bind(Include = "Id, Body")] Answer answer, int postId)
        {
            answer.Answerer = db.Users.Find(User.Identity.GetUserId());
                Post post = db.Posts.Find(postId);
                answer.ParentPost = post;
                answer.CorrectAnswer = false;
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = postId });
        }


        [HttpGet]
        public ActionResult MarkAsAnswer(int answerId, int postId)
        {
            Answer ExistingCorrectAnswer = db.Answers.Where(p => p.ParentPost.Id == postId)
                .Where(c => c.CorrectAnswer == true).FirstOrDefault();

            if(ExistingCorrectAnswer != null)
            {
                ExistingCorrectAnswer.CorrectAnswer = false;
            }

            Answer NewAnswer = db.Answers.Find(answerId);
            NewAnswer.ParentPost = db.Posts.Find(postId);
            NewAnswer.CorrectAnswer = true;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("Details", new { id = postId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(AnswerComment comment, int answerId, int postId)
        {
            Answer answer = db.Answers.Find(answerId);
            comment.ParentAnswer = answer;
            comment.Commenter = db.Users.Find(User.Identity.GetUserId());
            comment.CommenterName = comment.Commenter.FirstName + " " + comment.Commenter.Surname;
            db.AnswerComments.Add(comment);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = postId });
        }

        [HttpGet]
        public ActionResult ProcessLike(int answerId, int postId, string likeType)
        {
            Answer answer = db.Answers.Include(l => l.Likes).Where(a => a.Id == answerId).FirstOrDefault();
            string CurrentUserId = User.Identity.GetUserId();
            if (likeType == "like")
            {
                Like like = new Like
                {
                    LikedAnswer = answer,
                    Liker = db.Users.Find(CurrentUserId)
                };
                db.Likes.Add(like);
            }      
            else if (likeType == "unlike")
            {
                Like likeInDb = db.Likes.Where(a => a.LikedAnswer.Id == answerId)
                    .Where(u => u.Liker.Id == CurrentUserId).FirstOrDefault();

                db.Likes.Remove(likeInDb);
            }
            db.SaveChanges();

            return RedirectToAction("Details", new { id = postId });
        }






















        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Question,Description,RaiserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
