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
using eCommerce.API.Static_ErrorMessage;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartItemsController : ControllerBase
    {
        private readonly eCommerceDBContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<CartItemsController> logger;

        public CartItemsController(eCommerceDBContext context, IMapper mapper, ILogger<CartItemsController> logger)
        {
            _context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/CartItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemReadOnlyDto>>> GetCartItems()
        {
            try
            {
                if (_context.CartItems == null)
                {
                    return NotFound();
                }

                var cartItems = await _context.CartItems.ToListAsync();
                var cartItemDtos = mapper.Map<IEnumerable<CartItemReadOnlyDto>>(cartItems);
                return Ok(cartItemDtos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Perfoming GET in {nameof(GetCartItems)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/CartItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItemReadOnlyDto>> GetCartItem(int id)
        {
            throw new Exception("Testing Error log");
            try
            {
                if (_context.CartItems == null)
                {
                    return NotFound();
                }
                var cartItem = await _context.CartItems.FindAsync(id);

                if (cartItem == null)
                {
                    logger.LogWarning($"Record Not Found: {nameof(GetCartItem)} - ID: {id}");
                    return NotFound();
                }

                var cartItemDto = mapper.Map<CartItemReadOnlyDto>(cartItem);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Perfoming GET in {nameof(GetCartItems)}");
                return StatusCode(500, Messages.Error500Message);
            } 
        }

        // PUT: api/CartItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutCartItem(int id, CartItemUpdateDto cartItemDto)
        {
            if (id != cartItemDto.Id)
            {
                logger.LogWarning($"Update ID Invalid {nameof(PutCartItem)} - ID: {id}");
                return BadRequest();
            }

            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                logger.LogWarning($"{nameof(cartItem)} Record Not Found in {nameof(PutCartItem)} - ID: {id}");
                return NotFound();
            }

            mapper.Map(cartItemDto, cartItem);
            _context.Entry(cartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await CartItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    logger.LogError(ex, $"Error Perfoming GET in {nameof(PutCartItem)}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }

            return NoContent();
        }

        // POST: api/CartItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<CartItemCreateDto>> PostCartItem(CartItemCreateDto CartItemDto)
        {
            try
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
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error Perfoming POST in {nameof(PostCartItem)}", CartItemDto);
                return StatusCode(500, Messages.Error500Message);
            }
         
        }

        // DELETE: api/CartItems/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            try
            {
                if (_context.CartItems == null)
                {
                    return NotFound();
                }
                var cartItem = await _context.CartItems.FindAsync(id);
                if (cartItem == null)
                {
                    logger.LogWarning($"{nameof(cartItem)} Record Not Found In {nameof(DeleteCartItem)} - ID: {id}");
                    return NotFound();
                }

                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing Delete in {nameof(DeleteCartItem)} - ID: {id}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private async Task<bool> CartItemExists(int id)
        {
            return await _context.CartItems.AnyAsync(e => e.Id == id);
        }
    }
}
