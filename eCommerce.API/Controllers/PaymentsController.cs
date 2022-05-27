using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.API.Date;
using AutoMapper;
using eCommerce.API.Models_DTOs.Payment;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly eCommerceDBContext _context;
        private readonly IMapper mapper;

        public PaymentsController(eCommerceDBContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentReadOnlyDto>>> GetPayments()
        {
          if (_context.Payments == null)
          {
              return NotFound();
          }
            var payments = await _context.Payments.ToListAsync();
            var paymentsDtos = mapper.Map<IEnumerable<PaymentReadOnlyDto>>(payments);
            return Ok(paymentsDtos);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentReadOnlyDto>> GetPayment(int id)
        {
          if (_context.Payments == null)
          {
              return NotFound();
          }
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            var paymentDto = mapper.Map<PaymentReadOnlyDto>(payment);
            return Ok(paymentDto);
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutPayment(int id, PaymentUpdateDto paymentDto)
        {
            if (id != paymentDto.Id)
            {
                return BadRequest();
            }

            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            mapper.Map(paymentDto, payment);
            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<PaymentCreateDto>> PostPayment(PaymentCreateDto PaymentDto)
        {
          if (_context.Payments == null)
          {
              return Problem("Entity set 'eCommerceDBContext.Payments'  is null.");
          }
            var payment = mapper.Map<Payment>(PaymentDto);
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (_context.Payments == null)
            {
                return NotFound();
            }
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> PaymentExists(int id)
        {
            return await _context.Payments.AnyAsync(e => e.Id == id);
        }
    }
}
