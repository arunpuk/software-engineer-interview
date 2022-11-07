using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zip.InstallmentsApi.Services;

namespace Zip.InstallmentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallmentController : ControllerBase
    {
        private readonly ILogger<InstallmentController> _logger;
        private readonly IInstallmentService _installmentService;

        public InstallmentController(ILogger<InstallmentController> logger, IInstallmentService installmentService)
        {
            _logger = logger;
            _installmentService = installmentService;
        }


        [HttpGet]
        public async Task<IActionResult> GetInstallments(decimal purchasedAmount)
        {
            if(purchasedAmount <= 0)
                return BadRequest("Purchase amount should be greater than zero");

            var paymentPlan = await _installmentService.GetPaymentPlan(purchasedAmount);


            return Ok(paymentPlan);
        }
    }
}
