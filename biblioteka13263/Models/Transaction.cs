using System.Diagnostics.Eventing.Reader;

namespace biblioteka13263.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public Worker Worker { get; set; }
        public Book[] Books { get; set; }
        public DateTime Data {  get; set; }
        public int Status { get; set; }
        public bool Type { get; set; }
    //    public enum Statuses { Request= 1, Accepted = 2, Denied = 3 }
    }
}