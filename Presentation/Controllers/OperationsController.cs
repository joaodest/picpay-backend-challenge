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

        [HttpGet("api/transactions")]
        public async Task<IActionResult> GetTransactions()
        {
            try
            {
                var txs = await _bankingOperations.GetTransactions();
                return Ok(txs);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }

        [HttpGet("api/transactions/{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            try
            {
                var tx = await _bankingOperations.GetTransaction(id);
                return Ok(tx);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }



        [HttpDelete("api/transactions/{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            try
            {

                await _bankingOperations.DeleteTransaction(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }


        [HttpPost("api/transactions/transfer")]

        public async Task<IActionResult> Transfer(string fromUserDocument, string toUserDocument, double amount)
        {
            try
            {
                var transfer = await _bankingOperations.Transfer(fromUserDocument, toUserDocument, amount);
                return Ok(transfer);
            }
            catch (Exception e)
            {
                return BadRequest($"Error {e.Message}");
            }
        }

    }
}
