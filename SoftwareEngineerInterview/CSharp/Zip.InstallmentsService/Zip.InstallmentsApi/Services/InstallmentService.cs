using Zip.InstallmentsService;

namespace Zip.InstallmentsApi.Services
{
    public class InstallmentService : IInstallmentService
    {
        public async Task<PaymentPlan> GetPaymentPlan(decimal amount)
        {
            var paymentFactory = new PaymentPlanFactory();
            return await Task.FromResult(paymentFactory.CreatePaymentPlan(amount));
        }
    }
}
