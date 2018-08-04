using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineBookStore.BO
{
    public class BooksBO
    {
        OnlineBookStoreEntities context = new OnlineBookStoreEntities();
        public Book GetBook(int bookId)
        {
            return context.Books.Where(b => b.PKBookId == bookId).SingleOrDefault();
        }
        public List<Book> GetBooks(bool? isActive = null)
        {
            try
            {
                IQueryable<Book> qry = context.Books;
                if (isActive != null)
                {
                    qry = qry.Where(p => p.IsActive == isActive);
                }
              

                return qry.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void InsertBook(Book objBook)
        {
            try
            {
                objBook.FKAddedBy = Helper.UserId;
                context.Books.Add(objBook);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateBook(Book objBook)
        {
            try
            {
                objBook.FKAddedBy = Helper.UserId;
                context.Entry(objBook).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteBook(int bookId)
        {
            try
            {
                Book objBook = context.Books.Find(bookId);
                context.Books.Remove(objBook);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}