using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class ExpenseDetail
    {
        public ExpenseDetail()
        {
            Expense = new HashSet<Expense>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SourceId { get; set; }
        public decimal Amount { get; set; }

        public virtual Category Category { get; set; }
        public virtual Source Source { get; set; }
        public virtual ICollection<Expense> Expense { get; set; }
    }
}
