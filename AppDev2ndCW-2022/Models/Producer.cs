using System.ComponentModel.DataAnnotations;

namespace AppDev2ndCW_2022.Models
{
    public class Producer
    {
        [Key]
        public long ProducerNumber { get; set; }
        public string? ProducerName { get; set; }
    }
}
