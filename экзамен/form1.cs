using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CreditApp
{
    public partial class Form1 : Form
    {
        private List<Bank> banks;
        
        public Form1()
        {
            InitializeComponent();
            InitializeBanks();
        }

        private void InitializeBanks()
        {
            banks = new List<Bank>
            {
                new Bank("Сбербанк", 26, 5),
                new Bank("Россельхозбанк", 25, 8),
                new Bank("ВТБ 24", 28, 4),
                new Bank("Альфа-банк", 30, 6),
                new Bank("Райффайзенбанк", 24, 7)
            };
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Сбор данных
                double amount = Convert.ToDouble(txtAmount.Text);
                int months = Convert.ToInt32(txtMonths.Text);

                if (amount <= 0 || months <= 0)
                {
                    MessageBox.Show("Введите корректные положительные значения!");
                    return;
                }

                // Очистка списка
                listBoxResults.Items.Clear();

                // 2. Расчет для каждого банка
                foreach (var bank in banks)
                {
                    // Страховая сумма
                    double totalAmount = CreditCalculator.CalculateTotalWithInsurance(amount, bank.InsurancePercent);
                    
                    // Ежемесячный платеж
                    double payment = CreditCalculator.CalculateAnnuityPayment(totalAmount, months, bank.Rate);
                    
                    // Вывод в список
                    string result = $"{bank.Name}: {payment:F2} руб. (страховка: {bank.InsurancePercent}%)";
                    listBoxResults.Items.Add(result);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите числовые значения!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}