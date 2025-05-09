namespace OnlineStore.Models
{
    public class Cart
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }
        public User User { get; set; }
        public Guid UserID { get; set; }

        public Book Book { get; set; }
        public int BookId { get; set; }

        public DateTime DateCreate { get; set; }
    }
}
