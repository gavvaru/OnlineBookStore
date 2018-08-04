using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore;
using OnlineBookStore.BO;

namespace OnlineBookStore.Controllers
{
    public class BooksController : Controller
    {
        BooksBO booksBO = new BooksBO();

        // GET: Books
        public ActionResult Index()
        {
            var books = booksBO.GetBooks();
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = booksBO.GetBook((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            List<UserProfile> userProfiles = new UserProfileBO().GetUsers();
            ViewBag.FKAddedBy = new SelectList(userProfiles, "PKUserId", "FirstName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKBookId,BookName,BookDescription,BookImageURL,AuthorName,Price,OfferedPrice,Rating,FKAddedBy,IsActive")] Book book, HttpPostedFileBase BookImage)
        {
            if (ModelState.IsValid)
            {
                string extension = Path.GetExtension(BookImage.FileName);
                if(extension == ".jpeg" || extension == ".png" || extension == ".jpg")
                {
                    book.BookImageURL = Guid.NewGuid() + extension;
                    BookImage.SaveAs(Server.MapPath("~/BookImages/" + book.BookImageURL));
                    booksBO.InsertBook(book);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid File Format");
                    return View();
                }
            }
            List<UserProfile> userProfiles = new UserProfileBO().GetUsers();
            ViewBag.FKAddedBy = new SelectList(userProfiles, "PKUserId", "FirstName", book.FKAddedBy);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = booksBO.GetBook((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            List<UserProfile> userProfiles = new UserProfileBO().GetUsers();
            ViewBag.FKAddedBy = new SelectList(userProfiles, "PKUserId", "FirstName", book.FKAddedBy);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKBookId,BookName,BookDescription,BookImageURL,AuthorName,Price,OfferedPrice,Rating,FKAddedBy,IsActive")] Book book, HttpPostedFileBase BookImage)
        {
            if (ModelState.IsValid)
            {
                if(BookImage == null)
                {
                    booksBO.UpdateBook(book);
                    return RedirectToAction("Index");
                }
                string extension = Path.GetExtension(BookImage.FileName);
                if (extension == ".jpeg" || extension == ".png" || extension == ".jpg")
                {
                    book.BookImageURL = Guid.NewGuid() + extension;
                    BookImage.SaveAs(Server.MapPath("~/BookImages/" + book.BookImageURL));
                    booksBO.UpdateBook(book);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid File Format");
                    return View();
                }
            }
            List<UserProfile> userProfiles = new UserProfileBO().GetUsers();
            ViewBag.FKAddedBy = new SelectList(userProfiles, "PKUserId", "FirstName", book.FKAddedBy);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = booksBO.GetBook((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            booksBO.DeleteBook(id);
            return RedirectToAction("Index");
        }
        
    }
}
