using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Dtos
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime MembershipDate { get; set; }
    }

    public class MemberAddDto 
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime MembershipDate { get; set; } = DateTime.Now;
    }

    public class MemberUpdateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime MembershipDate { get; set; }
    }

    public class MemberDeleteDto
    {
        public int Id { get; set; }
    }

    public class MemberSearchDto
    {
        public string? FullName { get; set; } = "";
        public string? Email { get; set; } = "";
    }
}
