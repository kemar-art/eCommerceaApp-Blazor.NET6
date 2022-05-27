using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.API.Date;
using AutoMapper;
using eCommerce.API.Models_DTOs.Supplier;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SuppliersController : ControllerBase
    {
        private readonly eCommerceDBContext _context;
        private readonly IMapper mapper;

        public SuppliersController(eCommerceDBContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierReadOnlyDto>>> GetSuppliers()
        {
          if (_context.Suppliers == null)
          {
              return NotFound();
          }
            var suppliers = await _context.Suppliers.ToListAsync();
            var suppliersDtos = mapper.Map<IEnumerable<SupplierReadOnlyDto>>(suppliers);
            return Ok(suppliersDtos);
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierReadOnlyDto>> GetSupplier(int id)
        {
          if (_context.Suppliers == null)
          {
              return NotFound();
          }
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            var supplierDto = mapper.Map<SupplierReadOnlyDto>(supplier);
            return supplierDto;
        }

        // PUT: api/Suppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutSupplier(int id, SupplierUpdateDto supplierDto)
        {
            if (id != supplierDto.Id)
            {
                return BadRequest();
            }

            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            mapper.Map(supplierDto, supplier);
            _context.Entry(supplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SupplierExists(id))
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

        // POST: api/Suppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<SupplierCreateDto>> PostSupplier(SupplierCreateDto SupplierDto)
        {
          if (_context.Suppliers == null)
          {
              return Problem("Entity set 'eCommerceDBContext.Suppliers'  is null.");
          }
            var supplier = mapper.Map<Supplier>(SupplierDto);
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSupplier), new { id = supplier.Id }, supplier);
        }

        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            if (_context.Suppliers == null)
            {
                return NotFound();
            }
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> SupplierExists(int id)
        {
            return await _context.Suppliers.AnyAsync(e => e.Id == id);
        }
    }
}
