namespace Ex4.BusinessLogic.DTO
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<BookDto> Books { get; set; } = new();
    }
}
