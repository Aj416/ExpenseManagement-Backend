using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Expense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DetailId { get; set; }

        public virtual ExpenseDetail Detail { get; set; }
    }
}
