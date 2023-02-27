using JWT_Sample.DAO;
using JWT_Sample.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT_Sample.Repositories.Implements
{
    public class AuthorRepository : IAuthorRepository
    {
        // public AuthorRepository()
        // {
        //     try
        //     {
        //         var _dbContext = new DBContext();
        //         var authors = new List<Author>{
        //             new Author{
        //                 Name = "Bui Duc Trung",
        //                 Books = new List<Book>{
        //                     new Book{Name = "Education Data Mining"},
        //                     new Book{Name = "Let Him Cook"},
        //                     new Book{Name = "Who Let Him Cook"}
        //                 }
        //             },
        //             new Author{
        //                 Name = "Hoang Quoc Anh",
        //                 Books = new List<Book>{
        //                     new Book{Name = "Gym Every Day"},
        //                     new Book{Name = "How To Lose Weight"}
        //                 }
        //             }
        //         };
        //         _dbContext.Authors?.AddRange(authors);
        //         _dbContext.SaveChanges();
        //     }
        //     catch (Exception e)
        //     {
        //         throw new Exception(e.Message);
        //     }
        // }

        public void AddAuthor(Author author)
        {
            try
            {
                var _dbContext = new DBContext();
                _dbContext.Authors?.Add(author);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteAuthor(Author author)
        {
            try
            {
                var _dbContext = new DBContext();
                if (_dbContext.Authors?.FirstOrDefault(auth => auth.Id == author.Id) is var authorDel && authorDel != null)
                {
                    _dbContext.Authors?.Remove(authorDel);
                }
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Author> GetAuthor()
        {
            List<Author>? authors = new List<Author>();
            try
            {
                var _dbContext = new DBContext();
                authors = _dbContext.Authors?.Include(a => a.Books).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return authors ?? new List<Author>();
        }

        public void UpdateAuthor(Author author)
        {
            try
            {
                var _dbContext = new DBContext();
                if (_dbContext.Authors?.Include(auth => auth.Books).FirstOrDefault(auth => author.Id == auth.Id) is var authorUpdate && authorUpdate != null)
                {
                    authorUpdate.Name = author.Name;
                    authorUpdate.Books = author.Books;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}