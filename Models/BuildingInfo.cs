namespace vuejs_backend.Models
{
    public class BuildingInfo
    {
        public int id { get; set; }
        public int idTag { get; set; }
        public int buildingNo { get; set; }
        public string? buildingName { get; set; }
    }
    public class BuildingData
    {
        public int id { get; set; }
        public int id_building { get; set; }
        public string? part { get; set; }

        public DateTime? openDate {get; set; }
    }
}