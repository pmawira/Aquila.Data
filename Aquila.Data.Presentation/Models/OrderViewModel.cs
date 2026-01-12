namespace Aquila.Data.Presentation.Models
{
    public class OrderViewModel
    {
        public int Id{ get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        //Derived from JOIN
        public string CustomerName { get; set; } = "";
    }
}
