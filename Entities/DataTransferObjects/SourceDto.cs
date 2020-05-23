using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class SourceDto
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }
    }
}
