using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PicpayChallenge.Application.Interfaces;

namespace PicpayChallenge.Presentation.Controllers
{
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IBankingOperations _bankingOperations;

        public OperationsController(IBankingOperations bankingOperations)
        {
            _bankingOperations = bankingOperations;
        }


        [HttpPost("api/transfer")]

        public async Task<IActionResult> Transfer(string fromUserDocument, string toUserDocument, double amount)
        {
            try
            {
                var transfer = await _bankingOperations.Transfer(fromUserDocument, toUserDocument, amount);
                return Ok(transfer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
