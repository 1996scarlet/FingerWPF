using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIngerWPF.Model
{
    public class FingerInfo
    {
        [Key]
        public int FingerId { get; set; }

        [Required]
        public string FingerString { get; set; }

        public string AccountName { get; set; }
        public virtual AccountInfo FingerAccount { get; set; }
    }
}
