using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BezdarAPI.DataContext.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int PermissionId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Salary { get; set; }
        public string ShopTitle {  get; set; }


        [ForeignKey("PermissionId")]
        public UserPermission UserPermission { get; set; }
    }
}
