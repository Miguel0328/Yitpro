using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime LevelChangeDate { get; set; }
        public long LevelId { get; set; }
        public LevelModel Level { get; set; }
        public int UserTypeId { get; set; }
        public UserTypeModel UserType { get; set; }
        public long ManagerId { get; set; }
        public UserModel Manager { get; set; }
        public long DepartmentId { get; set; }
        public GeneralCatalogMoldel Department { get; set; }
        public int BlockingAttempts { get; set; }
        public decimal Cost { get; set; }
        public bool Capture { get; set; }
        public bool Active { get; set; }
        public bool Locked { get; set; }
        public string Password { get; set; }
        public DateTime DatePassword { get; set; }
        public long UserCreateId { get; set; }
        public DateTime DateCreate { get; set; }
        public long UserModifiedId { get; set; }
        public DateTime DateModified { get; set; }
    }
}
