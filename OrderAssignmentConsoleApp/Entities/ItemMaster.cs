using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAssignmentConsoleApp.Entities
{
    public class ItemMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public override string ToString()
        {
            return "" + Id + " " + Name + " " + Price + " " + Quantity + "";
        }
    }
}
