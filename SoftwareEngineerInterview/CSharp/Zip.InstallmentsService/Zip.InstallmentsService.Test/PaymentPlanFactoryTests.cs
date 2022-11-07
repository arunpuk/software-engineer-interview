using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Zip.InstallmentsService.Test
{
    public class PaymentPlanFactoryTests
    {
        [Fact]
        public void WhenCreatePaymentPlanWithValidOrderAmount_ShouldReturnValidPaymentPlan()
        {
            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory();

            // Act
            var paymentPlan = paymentPlanFactory.CreatePaymentPlan(123.45M);

            // Assert
            paymentPlan.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-500)]
        public void ValidInstallments(decimal value)
        {
            var paymentPlanFactory = new PaymentPlanFactory();

            var paymentPlan = paymentPlanFactory.CreatePaymentPlan(value);
            var result = paymentPlan == null;

            Assert.True(result, $"Amount {value} should be valid");
        }

        [Theory]
        [InlineData(100)]
        [InlineData(123.45)]
        public void ValidInstallmentAmountAndDate(decimal value)
        {
            var paymentPlanFactory = new PaymentPlanFactory();

            var paymentPlan = paymentPlanFactory.CreatePaymentPlan(value);

            value = Math.Round(value);
            var result = paymentPlan.Installments.Count() == 4 &&
                paymentPlan.Installments[0].Amount == Math.Round(value / 4, 2) && paymentPlan.Installments[0].DueDate.Date.Equals(DateTime.UtcNow.Date) &&
                paymentPlan.Installments[1].Amount == Math.Round(value / 4, 2) && paymentPlan.Installments[1].DueDate.Date.Equals(DateTime.UtcNow.AddDays(14).Date) &&
                paymentPlan.Installments[2].Amount == Math.Round(value / 4, 2) && paymentPlan.Installments[2].DueDate.Date.Equals(DateTime.UtcNow.AddDays(28).Date) &&
                paymentPlan.Installments[3].Amount == Math.Round(value / 4, 2) && paymentPlan.Installments[3].DueDate.Date.Equals(DateTime.UtcNow.AddDays(42).Date);


            Assert.True(result, $"Converted {value} to 4 installment and Installment Amount {Math.Round(value / 4, 2)}");
        }
    }
}
