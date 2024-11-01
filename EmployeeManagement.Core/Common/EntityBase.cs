using System.Text.Json.Serialization;

namespace EmployeeManagement.Core.Common
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
    }
}
