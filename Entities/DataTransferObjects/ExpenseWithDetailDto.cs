using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class ExpenseWithDetailDto
    {
        public int ExpenseId { get; set; }
        public DateTime Date { get; set; }
        public int ExpenseDetailId { get; set; }
        public int CategoryId { get; set; }
        public int SourceId { get; set; }
        public decimal Amount { get; set; }
    }
}
