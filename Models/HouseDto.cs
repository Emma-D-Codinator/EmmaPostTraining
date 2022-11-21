using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HouseDto
    {
        public int ID { get; set; }

        public string? House_name_type { get; set; }

        public string? Address { get; set; }

        public double? Footage { get; set; }

        public byte[]? HouseImage { get; set; }

        public double? HouseCost { get; set; }

    }
}
