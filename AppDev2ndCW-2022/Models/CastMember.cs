using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDev2ndCW_2022.Models
{
    public class CastMember
    {
        [Key, Column(Order = 1)]
        public long DvdNumber { get; set; } 

        [Key, Column(Order = 2)]
        public long ActorNumber { get; set; }

        [ForeignKey("DvdNumber")]
        public DvdTitle DvdTitle { get; set; }

        [ForeignKey("ActorNumber")]
        public Actor Actor { get; set; }
    }
}
