using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.API.Date;
using eCommerce.API.Models_DTOs.Cart;
using AutoMapper;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly eCommerceDBContext _context;
        private readonly IMapper mapper;

        public CartsController(eCommerceDBContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartReadOnlyDto>>> GetCarts()
        {
          if (_context.Carts == null)
          {
              return NotFound();
          }
            var carts = await _context.Carts.ToListAsync();
            var cartsDtos = mapper.Map<IEnumerable<CartReadOnlyDto>>(carts);
            return Ok(cartsDtos);
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartReadOnlyDto>> GetCart(int id)
        {
          if (_context.Carts == null)
          {
              return NotFound();
          }
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            var cartDto = mapper.Map<CartReadOnlyDto>(cart);
            return Ok(cartDto);
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, CartUpdateDto cartDto)
        {
            if (id != cartDto.Id)
            {
                return BadRequest();
            }

            var cart = await _context.CartItems.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            mapper.Map(cartDto, cart);
            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CartCreateDto>> PostCart(CartCreateDto CartDto)
        {
          if (_context.Carts == null)
          {
              return Problem("Entity set 'eCommerceDBContext.Carts'  is null.");
          }
            var cart = mapper.Map<Cart>(CartDto);
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CartExists(int id)
        {
            return await _context.Carts.AnyAsync(e => e.Id == id);
        }
    }
}
