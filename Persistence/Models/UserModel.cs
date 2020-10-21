using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Persistence.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int RoleId { get; set; }
        public RoleModel Role { get; set; }
        public long? ManagerId { get; set; }
        public UserModel Manager { get; set; }
        public bool Active { get; set; }
        public bool Locked { get; set; }
        public string Password { get; set; }
        public DateTime PasswordLastUpdate { get; set; }
        public long? CreatedById { get; set; }
        public UserModel CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? UpdatedById { get; set; }
        public UserModel UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
