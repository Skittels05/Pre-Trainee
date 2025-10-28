namespace Ex4.BusinessLogic.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth  { get; set; }
        public ICollection<Book> Books { get;} = new List<Book>();
    }
}
