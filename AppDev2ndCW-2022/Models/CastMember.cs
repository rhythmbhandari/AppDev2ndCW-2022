using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDev2ndCW_2022.Models
{
    public class CastMember
    {
        public long DvdNumber { get; set; } 
        public long ActorNumber { get; set; }

        [ForeignKey("DvdNumber")]
        public DvdTitle DvdTitle { get; set; }

        [ForeignKey("ActorNumber")]
        public Actor Actor { get; set; }
    }
}
