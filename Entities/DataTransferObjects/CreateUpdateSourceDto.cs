using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class CreateUpdateSourceDto
    {
        [Required(ErrorMessage = "BankName is required")]
        [StringLength(50, ErrorMessage = "BankName can't be longer than 50 characters")]
        public string BankName { get; set; }
        [Required(ErrorMessage = "AccountHolderName is required")]
        [StringLength(50, ErrorMessage = "AccountHolderName can't be longer than 50 characters")]
        public string AccountHolderName { get; set; }
    }
}
