using System.ComponentModel.DataAnnotations;

namespace BezdarAPI.DataContext.Model
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string INN { get; set; }
    }
}
