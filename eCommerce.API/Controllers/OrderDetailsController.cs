using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.API.Date;
using AutoMapper;
using eCommerce.API.Models_DTOs.OrderDetail;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderDetailsController : ControllerBase
    {
        private readonly eCommerceDBContext _context;
        private readonly IMapper mapper;

        public OrderDetailsController(eCommerceDBContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailReadOnlyDto>>> GetOrderDetails()
        {
          if (_context.OrderDetails == null)
          {
              return NotFound();
          }
            var orderDetails = await _context.OrderDetails.ToListAsync();
            var orderDetailsDts = mapper.Map<IEnumerable<OrderDetailReadOnlyDto>>(orderDetails);
            return Ok(orderDetailsDts);
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailReadOnlyDto>> GetOrderDetail(int id)
        {
          if (_context.OrderDetails == null)
          {
              return NotFound();
          }
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            var orderDetailDto = mapper.Map<OrderDetailReadOnlyDto>(orderDetail);
            return Ok(orderDetailDto);
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetailUpdateDto orderDetailDto)
        {
            if (id != orderDetailDto.Id)
            {
                return BadRequest();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            mapper.Map(orderDetailDto, orderDetail);
            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderDetailExists(id))
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

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<OrderDetailCreateDto>> PostOrderDetail(OrderDetailCreateDto OrderDetailCreateDto)
        {
          if (_context.OrderDetails == null)
          {
              return Problem("Entity set 'eCommerceDBContext.OrderDetails'  is null.");
          }
            var orderDetail = mapper.Map<OrderDetail>(OrderDetailCreateDto);
            await _context.OrderDetails.AddAsync(orderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderDetail), new { id = orderDetail.Id }, orderDetail);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> OrderDetailExists(int id)
        {
            return await _context.OrderDetails.AnyAsync(e => e.Id == id);
        }
    }
}
