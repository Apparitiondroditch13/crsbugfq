namespace CreditApp
{
    public class Bank
    {
        public string Name { get; set; }
        public double Rate { get; set; } // Годовая ставка в %
        public double InsurancePercent { get; set; } // Страховка в %

        public Bank(string name, double rate, double insurancePercent)
        {
            Name = name;
            Rate = rate;
            InsurancePercent = insurancePercent;
        }
    }
}