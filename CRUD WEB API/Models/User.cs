using System.ComponentModel.DataAnnotations;

namespace CRUD_WEB_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public DateOnly CreateDate { get; set; }= new DateOnly();
    }
}
