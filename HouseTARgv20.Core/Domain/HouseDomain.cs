using System;

namespace HouseTARgv20.Core.Domain
{
    public class HouseDomain
    {
        public Guid? Id { get; set; }
        public string Address { get; set; }
        public int Floors { get; set; }
        public int Rooms { get; set; }
        public double Price { get; set; }
        public int HouseNumber { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
