using Zip.InstallmentsService;

namespace Zip.InstallmentsApi.Services
{
    public interface IInstallmentService
    {
        Task<PaymentPlan> GetPaymentPlan(decimal amount);
    }
}