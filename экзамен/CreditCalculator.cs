using System;

namespace CreditApp
{
    public static class CreditCalculator
    {
        /// <summary>
        /// Расчет аннуитетного платежа
        /// </summary>
        /// <param name="creditAmount">Сумма кредита</param>
        /// <param name="months">Срок в месяцах</param>
        /// <param name="annualRate">Годовая ставка (например, 20)</param>
        /// <returns>Ежемесячный платеж</returns>
        public static double CalculateAnnuityPayment(double creditAmount, int months, double annualRate)
        {
            if (creditAmount <= 0) throw new ArgumentException("Сумма должна быть больше нуля");
            if (months <= 0) throw new ArgumentException("Срок должен быть больше нуля");
            if (annualRate < 0) throw new ArgumentException("Ставка не может быть отрицательной");

            // Месячная ставка
            double monthlyRate = (annualRate / 12) / 100;
            
            // Коэффициент аннуитета: K = (S * (1+S)^M) / ((1+S)^M - 1)
            double pow = Math.Pow(1 + monthlyRate, months);
            double coefficient = (monthlyRate * pow) / (pow - 1);
            
            // Платеж
            return creditAmount * coefficient;
        }

        /// <summary>
        /// Расчет общей суммы с учетом страховки
        /// </summary>
        public static double CalculateTotalWithInsurance(double amount, double insurancePercent)
        {
            return amount * (1 + insurancePercent / 100);
        }
    }
}