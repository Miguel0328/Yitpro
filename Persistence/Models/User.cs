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
        public string SecondLastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Email { get; set; }
        public DateTime AdmissionDate { get; set; }
        public short RoleId { get; set; }
        public virtual RoleModel Role { get; set; }
        public long? ManagerId { get; set; }
        public virtual UserModel Manager { get; set; }
        public long? DepartmentId { get; set; }
        public virtual CatalogModel Department { get; set; }
        public bool Capture { get; set; }
        public bool Active { get; set; }
        public bool Locked { get; set; }
        public string Password { get; set; }
        public DateTime? PasswordLastUpdate { get; set; }
        public string Photo { get; set; }
        public long? UpdatedById { get; set; }
        public virtual UserModel UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<UserPermissionModel> Permissions { get; set; }
    }
}
