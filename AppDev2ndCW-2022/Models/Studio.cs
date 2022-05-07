using System.ComponentModel.DataAnnotations;

namespace AppDev2ndCW_2022.Models
{
    public class Studio
    {
        [Key]
        public long StudioNumber { get; set; }
        public string? StudioName { get; set; }

    }
}
