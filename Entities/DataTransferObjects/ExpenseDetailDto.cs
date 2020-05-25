namespace Entities.DataTransferObjects
{
    public class ExpenseDetailDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SourceId { get; set; }
        public decimal Amount { get; set; }
    }
}
