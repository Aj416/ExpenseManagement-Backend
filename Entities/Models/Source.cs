using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Source
    {
        public Source()
        {
            ExpenseDetail = new HashSet<ExpenseDetail>();
        }

        public int Id { get; set; }
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<ExpenseDetail> ExpenseDetail { get; set; }
    }
}
