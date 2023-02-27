using JWT_Sample.Models;
namespace JWT_Sample.Repositories
{
    public interface IAuthorRepository
    {
        public List<Author> GetAuthor();
        public void AddAuthor(Author author);
        public void UpdateAuthor(Author author);
        public void DeleteAuthor(Author author);
    }
}