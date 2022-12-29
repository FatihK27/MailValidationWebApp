using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Huawei.WebUIMailValidate.Models
{
    public class Validation
    {
        [Key]
        public int recID { get; set; }

        public string userID { get; set; }
        public string mailAddress { get; set; }
        public DateTime? RequestDate { get; set; }

        public DateTime? ResultDate { get; set; }
        public string? Result { get; set; }
        public string? ResultDescription { get; set; }
        public Guid BatchId { get; set; }

    }
}
