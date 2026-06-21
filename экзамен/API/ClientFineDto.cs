using System;

namespace CreditAPI.DTOs
{
    public class ClientFineDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal FineAmount { get; set; }
    }
}