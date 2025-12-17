using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Spot
{
    public class SpotDto
    {
        public int Id { get; set; }
        public string SpotNumber { get; set; }
        public bool IsOccupied { get; set; }
    }
}
