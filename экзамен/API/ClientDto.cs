using System;

namespace CreditAPI.DTOs
{
    // Для метода "Клиенты со штрафами"
    public class ClientFineDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal FineAmount { get; set; }
    }

    // Для метода "Все клиенты"
    public class ClientAllDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal AverageSalary { get; set; }
    }
}