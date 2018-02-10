using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.DAL;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Recipes.ToList());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipeID,Title,Author,Date,RecipeContent,Picute,Video")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recipe);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipeID,Title,Author,Date,RecipeContent,Picute,Video")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
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

        // POST: /Post/AddComment
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment([Bind(Include = "CommentID,RecipeID,Title,Author,CommentContent,Recipe")] Comment comment)
        {
            if (comment != null)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index", "Blog");
            }
            return View("~/Views/Blog/Index.cshtml", db.Recipes.ToList());
        }

        // POST: /Post/AddComment
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchTitle(String str)
        {
            if (str == null || str.Equals(""))
            {
                var Recipess = db.Recipes.ToList();
                return View("~/Views/Blog/Index.cshtml", Recipess);
            }
            var Recipes = db.Recipes.Where(recipe => recipe.Title.Equals(str)).ToList();
            if (Recipes.Count == 0)
            {
                ViewBag.Massage = "No matching title.";
                return View("~/Views/Blog/NotFoundInSearch.cshtml");
            }
            return View("~/Views/Blog/Index.cshtml", Recipes);
        }

        public ActionResult Statistics()
        {

            // INNER JOIN 1
            var innerJoinQuery1 = from r in db.Recipes
                                 join c in db.Comments on r.RecipeID equals c.RecipeID
                                 where r.Author == c.Author
                                 select new { AuthorName = r.Author };

            String innerJoinQuerystr1 = "<h4>";
            foreach(var name in innerJoinQuery1)
            {

                innerJoinQuerystr1 += name.AuthorName;
                innerJoinQuerystr1 += "<br /><br />";
            }
            innerJoinQuerystr1 += "</h4>";
            ViewBag.Authors = innerJoinQuerystr1;


            // INNER JOIN 2
            var innerJoinQuery2 = from r in db.Recipes
                                  join c in db.Comments on r.RecipeID equals c.RecipeID
                                  where r.Title == c.Title
                                  select new { TitleName = r.Title };

            String innerJoinQuerystr2 = "<h4>";
            foreach (var name in innerJoinQuery2)
            {

                innerJoinQuerystr2 += name.TitleName;
                innerJoinQuerystr2 += "<br /><br />";
            }
            innerJoinQuerystr2 += "</h4>";
            ViewBag.Titles = innerJoinQuerystr2;


            // GROUP BY
            var groupByQuery = from comment in db.Comments
                               group comment by comment.Author;
            String groupByQuerystr = "<h4>";
            foreach (IGrouping<string, Comment> group in groupByQuery)
            {
                groupByQuerystr += group.Key;
                groupByQuerystr += " : ";
                groupByQuerystr += group.Count().ToString();
                groupByQuerystr += "<br /><br />";

            }
            groupByQuerystr += "</h4>";
            ViewBag.CommentsAuthors = groupByQuerystr;

            return View();
        }

        public ActionResult SearchRecipe(String Title,String Author, String RecipeContent)
        {
            var Recipes = db.Recipes.Where(recipe => recipe.Title.Equals(Title) && recipe.Author.Equals(Author) && recipe.RecipeContent.Equals(RecipeContent)).ToList();

            return View("~/Views/Blog/Index.cshtml", Recipes);
        }

        public ActionResult Search()
        {
            return View();
        }
    }

}
