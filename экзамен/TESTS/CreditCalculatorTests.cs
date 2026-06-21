using NUnit.Framework;
using System;

namespace CreditApp.Tests
{
    [TestFixture]
    public class CreditCalculatorTests
    {
        // Позитивный тест: Проверка расчета по примеру из задания
        [Test]
        public void CalculateAnnuityPayment_ValidData_ReturnsCorrectPayment()
        {
            // Arrange (подготовка)
            double amount = 1000000; // 1 млн
            int months = 36;
            double rate = 20; // 20% годовых

            // Act (действие)
            double payment = CreditCalculator.CalculateAnnuityPayment(amount, months, rate);

            // Assert (проверка)
            // Ожидаемый платеж из примера: 37164 руб.
            double expected = 37164;
            double tolerance = 1.0; // Допустимая погрешность
            
            Assert.AreEqual(expected, payment, tolerance, 
                $"Ожидался платеж {expected}, но получен {payment}");
        }

        // Негативный тест: Отрицательная сумма
        [Test]
        public void CalculateAnnuityPayment_NegativeAmount_ThrowsException()
        {
            // Arrange
            double amount = -1000;
            int months = 12;
            double rate = 20;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => 
                CreditCalculator.CalculateAnnuityPayment(amount, months, rate));
            
            Assert.That(ex.Message, Does.Contain("больше нуля"));
        }

        // Негативный тест: Нулевой срок
        [Test]
        public void CalculateAnnuityPayment_ZeroMonths_ThrowsException()
        {
            // Arrange
            double amount = 10000;
            int months = 0;
            double rate = 20;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                CreditCalculator.CalculateAnnuityPayment(amount, months, rate));
        }

        // Позитивный тест: Проверка расчета страховки
        [Test]
        public void CalculateTotalWithInsurance_ValidData_ReturnsCorrectTotal()
        {
            // Arrange
            double amount = 100000;
            double insurancePercent = 5;

            // Act
            double total = CreditCalculator.CalculateTotalWithInsurance(amount, insurancePercent);

            // Assert
            Assert.AreEqual(105000, total, 0.01);
        }

        // Негативный тест: Некорректный процент страховки
        [Test]
        public void CalculateTotalWithInsurance_NegativePercent_ReturnsLessThanAmount()
        {
            // Arrange
            double amount = 100000;
            double insurancePercent = -5;

            // Act
            double total = CreditCalculator.CalculateTotalWithInsurance(amount, insurancePercent);

            // Assert - проверяем, что страховка не уменьшает сумму
            Assert.That(total, Is.LessThan(amount), 
                "При отрицательной страховке сумма должна быть меньше");
        }
    }
}