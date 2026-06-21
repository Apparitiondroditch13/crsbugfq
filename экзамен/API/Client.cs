using System;

namespace CreditAPI.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal AverageSalary { get; set; }
        public bool HasUnpaidFines { get; set; }
        public decimal FineAmount { get; set; } // Сумма штрафа
    }
}