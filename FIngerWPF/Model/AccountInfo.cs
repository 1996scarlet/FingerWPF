namespace FIngerWPF.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.ComponentModel;
    public class AccountInfo
    {
        [Key]
        public string AccountName { get; set; }

        public virtual ICollection<FingerInfo> AccountFingerInfoes { get; set; }
    }
}
