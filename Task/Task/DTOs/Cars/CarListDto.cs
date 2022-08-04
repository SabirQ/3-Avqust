using System.Collections.Generic;

namespace Task.DTOs.Cars
{
    public class CarListDto
    {
        public List<CarListItemDto> CarListItemDtos { get; set; }
        public int TotalCount { get; set; }
    }
}
