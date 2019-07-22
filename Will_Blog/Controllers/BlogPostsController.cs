using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Will_Blog.Models;
using Will_Blog.Utilities;

namespace Will_Blog.Controllers
{
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts

        //This is my BlogPosts Index for the public to see. Nobody sees Posts that are not marked as published
        public ActionResult Index()
        {
            return View(db.Posts.ToList());//Take(1)// db.Posts.Where(b => b.Published).OrderByDescending(b => b.Created.)ToList(); b.Published=true
        }

        //[Authorize(Roles ="Admin")]
        public ActionResult AdminIndex()//Do not have "AdminIndex view"
        {
            return View("Index", db.Posts.ToList());//But Return Index View for admin only
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(string slug)//signature = collection of methods an action is expecting(nullable int/int?)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.FirstOrDefault(p => p.Slug == slug);//Find = Primary key only
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Abstract,Body,Published")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            { 
            var slug = StringUtilities.MakeSlug(blogPost.Title);

 
                if (string.IsNullOrWhiteSpace(slug))
                {
                    ModelState.AddModelError("Title", "Invalid title");
                    return View(blogPost);
                }

                if (db.Posts.Any(p => p.Slug == slug))
                {
                    ModelState.AddModelError("Title", "The Title Must Be Unique");
                    return View(blogPost);
                }
                //var slugMaker = new StringUtilities();
                //var slug = slugMaker.MakeSlug(blogPost.Title);

                blogPost.Slug = slug;
                blogPost.Created = DateTimeOffset.Now;
                db.Posts.Add(blogPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        //[Authorize(Roles ="Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Slug,Abstract,Body,MediaUrl,Published,,Created,Updated")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
            var newSlug = StringUtilities.MakeSlug(blogPost.Slug);

            if(newSlug !=blogPost.Slug)
            {
                if(string.IsNullOrWhiteSpace(newSlug))
                {
                    ModelState.AddModelError("Title", "Invalid title");
                    return View(blogPost);
                }

                if(db.Posts.Any(p => p.Slug == newSlug))
                {
                    ModelState.AddModelError("Title", "The Title Must Be Unique");
                    return View(blogPost);
                }
                                                            
            }
                blogPost.Slug = newSlug;
                blogPost.Updated = DateTimeOffset.Now;
                db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.Posts.Find(id);
            db.Posts.Remove(blogPost);
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
