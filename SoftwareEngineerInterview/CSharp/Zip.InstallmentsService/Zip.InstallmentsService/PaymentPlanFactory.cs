using System;

namespace Zip.InstallmentsService
{
    /// <summary>
    /// This class is responsible for building the PaymentPlan according to the Zip product definition.
    /// </summary>
    public class PaymentPlanFactory
    {
        /// <summary>
        /// Builds the PaymentPlan instance.
        /// </summary>
        /// <param name="purchaseAmount">The total amount for the purchase that the customer is making.</param>
        /// <returns>The PaymentPlan created with all properties set.</returns>
        public PaymentPlan CreatePaymentPlan(decimal purchaseAmount)
        {
            if (purchaseAmount <= 0)
                return null;

            var paymentPlan = new PaymentPlan
            {
                Id = Guid.NewGuid(),
                PurchaseAmount = purchaseAmount,
                Installments = ConvertToInstallment(4, 14, purchaseAmount)
            };


            return paymentPlan;
        }

        private Installment[] ConvertToInstallment(int noOfInstallment, int intervalInDays, decimal purchaseAmount)
        {
            var intallments = new Installment[noOfInstallment];
            var installmentDate = DateTime.UtcNow;

            var installmentNumber = 0;
            purchaseAmount = Math.Round(purchaseAmount);

            try
            {

                while (installmentNumber < noOfInstallment)
                {
                    intallments[installmentNumber] = new Installment()
                    {
                        Id = Guid.NewGuid(),
                        Amount = Math.Round(purchaseAmount / noOfInstallment,2),
                        DueDate = installmentDate.Date
                    };

                    installmentDate = installmentDate.AddDays(intervalInDays);
                    installmentNumber++;
                }

                return intallments;

            }
            catch(Exception exp)
            {
                throw new Exception($"Exception in ConvertToInstallment in {this}", exp);
            }
        }
    }
}
