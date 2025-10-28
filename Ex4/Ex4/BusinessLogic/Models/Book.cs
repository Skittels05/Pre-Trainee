using System.Text.Json.Serialization;

namespace Ex4.BusinessLogic.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublishedYear {  get; set; }
        public int AuthorId {  get; set; }
        [JsonIgnore]
        public Author? Author { get; set; }
    }
}
