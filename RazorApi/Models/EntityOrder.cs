namespace RazorApi.Models
{
    public class EntityOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Amount { get; set; }

        public string TransactionId { get; set; }

        public string OrderId { get; set; }


    }
}
