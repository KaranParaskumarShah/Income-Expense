using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public TransactionController(Context context)
        {
            Context = context;
        }
        public Context Context { get; set; }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert(FinancialTransaction financialTransaction)
        {
            financialTransaction.CreatedOn = DateTime.Now;
            Context.Transactions.Update(financialTransaction);
            await Context.SaveChangesAsync();
            return Ok(financialTransaction);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await Context.Transactions.OrderByDescending(x => x.Id).ToListAsync();

            return Ok(transactions);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(Context.Transactions.Any(t => t.Id == id))
            {
                var transaction = Context.Transactions.Single(t => t.Id == id);
                Context.Transactions.Remove(transaction);
                await Context.SaveChangesAsync();
                return Ok("deleted");
            }
            return Ok("not deleted");
         
        }

        [HttpGet("GetMonthlyTransactions")]
        public async Task<IActionResult> GetMothlyTransactions()
        {
            var transactions = await Context.Transactions.ToListAsync();

            var monthlyTransactions =
                transactions
                .GroupBy(k => new { k.Date.Year, k.Date.Month },
                         e => e,
                         (key, group) => new { key.Year, Month = key.Month - 1, Transactions = group.ToList() })
                .OrderByDescending(e => e.Year)
                .ThenByDescending(e => e.Month)
                .ToList();

            return Ok(monthlyTransactions);
        }



    }
}
