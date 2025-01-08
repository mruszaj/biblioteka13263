namespace biblioteka13263.Models
{
    public class Book
    {
        public int ISBN { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public Genre[] Genres  { get; set; }
        public bool IsAvilible {  get; set; }
        public DateTime WhenAvilable { get; set; }
    }
}