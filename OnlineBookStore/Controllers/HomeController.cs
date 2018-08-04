using OnlineBookStore.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBookStore.Controllers
{
    public class HomeController : Controller
    {
        BooksBO booksBO = new BooksBO();
        public ActionResult Index()
        {
            List<Book> books = booksBO.GetBooks(true);
            return View(books);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}