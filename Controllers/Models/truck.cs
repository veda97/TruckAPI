namespace TruckInfoApi.Controllers.Models
{
    public class Truck
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Owner { get; set; }
        public string Capacity { get; set; }
        public string Route { get; set; }
    }
}
