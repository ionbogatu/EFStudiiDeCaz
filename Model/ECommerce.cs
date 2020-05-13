using System.ComponentModel.DataAnnotations.Schema;

namespace EFStudiiDeCaz.Model
{
    [Table("Business", Schema = "Database")]
    public class ECommerce : Business
    {
        public string URL { get; set; }
    }
}
