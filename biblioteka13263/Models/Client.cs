namespace biblioteka13263.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public Book[] Books { get; set; }
        public string Password { get; set; }

    }
}