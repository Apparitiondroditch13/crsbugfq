using System;

namespace CreditAPI.DTOs
{
    public class ClientAllDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal AverageSalary { get; set; }
    }
}