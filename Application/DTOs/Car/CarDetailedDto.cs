using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Car
{
    public class CarDetailedDto
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
    }
}
