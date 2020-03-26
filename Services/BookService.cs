using BooksApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _book;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _book = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
            _book.Find(book => true).ToList();

        public Book Get(string id) =>
            _book.Find(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _book.InsertOne(book);
            return book;
        }

        public Book Update(string id, Book bookIn)
        {
            _book.ReplaceOne(book => book.Id == id, bookIn);
            return bookIn;
        }

        public void Remove(Book bookIn) =>
            _book.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _book.DeleteOne(book => book.Id == id);
    }
}