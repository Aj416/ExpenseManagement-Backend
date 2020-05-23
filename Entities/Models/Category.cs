using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Category
    {
        public Category()
        {
            ExpenseDetail = new HashSet<ExpenseDetail>();
        }

        public int Id { get; set; }
        public string Category1 { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<ExpenseDetail> ExpenseDetail { get; set; }
    }
}
