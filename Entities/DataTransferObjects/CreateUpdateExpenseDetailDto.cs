using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class CreateUpdateExpenseDetailDto
    {
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Source is required")]
        public int SourceId { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }
    }
}
