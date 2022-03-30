using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")] //api/todo
    [ApiController]

    public class PaymentDetailController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PaymentDetailController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentDatas()
        {
            var paymentdatas = await _context.PaymentDatas.ToListAsync();
            return Ok(paymentdatas);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(PaymentData data)
        {
            if(ModelState.IsValid)
            {
                await _context.PaymentDatas.AddAsync(data);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetItem", new {data.Id}, data);
            }

            return new JsonResult("Something went wrong") {StatusCode = 500};
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _context.PaymentDatas.FirstOrDefaultAsync(x => x.Id  == id);

            if(item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, PaymentData item)
        {
            if(id != item.Id )
                return BadRequest();
                
            var existItem = await _context.PaymentDatas.FirstOrDefaultAsync(x => x.Id  == id);

            if(existItem == null)
                return NotFound();
                
            existItem.cardOwnerName = item.cardOwnerName;
            existItem.expirationDate = item.expirationDate;
            existItem.securityCode = item.securityCode;

            //Implement the changes on the database level
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var existItem = await _context.PaymentDatas.FirstOrDefaultAsync(x => x.Id == id);

            if(existItem == null)
                return NotFound();
                
            _context.PaymentDatas.Remove(existItem);
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }
    }
}
