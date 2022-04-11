using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDev2ndCW_2022.Models
{
    public class DvdCopy
    {
        [Key]
        public long CopyNumber { get; set; }

        public long DvdNumber { get; set; }

        public DateTime DatePurchased { get; set; }

        [ForeignKey("DvdNumber")]
        public DvdTitle DvdTitle { get; set; }
    }
}
