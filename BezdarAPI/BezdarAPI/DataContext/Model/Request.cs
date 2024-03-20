using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BezdarAPI.DataContext.Model
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShopId { get; set; }


        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }
    }
}
