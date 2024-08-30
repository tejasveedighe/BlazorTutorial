using EmployeeManagement.Models;

namespace EmployeeManangement.API.Models
{
    public class PostEmployeeDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int DeptId { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhotoPath { get; set; }
    }
}
