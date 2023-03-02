using Restaurant.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Restaurant.Domain
{
    public class Customer:BaseEntity
    {
        //using column type to ensure during migrations maximum characters are not initialized for name
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<OrderMaster> OrderMasters { get; set; } = new();

    }
}
