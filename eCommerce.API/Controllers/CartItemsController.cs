using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.API.Date;
using eCommerce.API.Models_DTOs.CartItem;
using AutoMapper;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly eCommerceDBContext _context;
        private readonly IMapper mapper;

        public CartItemsController(eCommerceDBContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/CartItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemReadOnlyDto>>> GetCartItems()
        {
          if (_context.CartItems == null)
          {
              return NotFound();
          }
            var cartItems = await _context.CartItems.ToListAsync();
            var cartItemDtos = mapper.Map<IEnumerable<CartItemReadOnlyDto>>(cartItems);
            return Ok(cartItemDtos);
        }

        // GET: api/CartItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItemReadOnlyDto>> GetCartItem(int id)
        {
          if (_context.CartItems == null)
          {
              return NotFound();
          }
            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            var cartItemDto = mapper.Map<CartItemReadOnlyDto>(cartItem);
            return Ok(cartItemDto);
        }

        // PUT: api/CartItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartItem(int id, CartItemUpdateDto cartItemDto)
        {
            if (id != cartItemDto.Id)
            {
                return BadRequest();
            }

            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            mapper.Map(cartItemDto, cartItem);
            _context.Entry(cartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CartItemExists(id))
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

        // POST: api/CartItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CartItemCreateDto>> PostCartItem(CartItemCreateDto CartItemDto)
        {
          if (_context.CartItems == null)
          {
              return Problem("Entity set 'eCommerceDBContext.CartItems'  is null.");
          }
            var cartItem = mapper.Map<CartItem>(CartItemDto);
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.Id }, cartItem);
        }

        // DELETE: api/CartItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            if (_context.CartItems == null)
            {
                return NotFound();
            }
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CartItemExists(int id)
        {
            return await _context.CartItems.AnyAsync(e => e.Id == id);
        }
    }
}
