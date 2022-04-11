using System;
using System.ComponentModel.DataAnnotations;

namespace AppDev2ndCW_2022.Models
{
    public class Actor
    {
        [Key]
        public long ActorNumber { get; set; }
        public string ActorSurname { get; set; }
        public string ActorFirstName { get; set; }

    }
}
