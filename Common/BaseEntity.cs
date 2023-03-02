using System.ComponentModel.DataAnnotations;

namespace Restaurant.Common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
