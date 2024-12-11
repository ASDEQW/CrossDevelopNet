using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OilCompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly OilCompanyService _oilCompanyService;

        public OilCompaniesController(ApplicationDbContext context, OilCompanyService oilCompanyService)
        {
            _context = context;
            _oilCompanyService = oilCompanyService;
        }

     
        // �������� ��� �������� � �� ����������� � IT
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OilCompanyDTO>>> GetOilCompanies()
        {
            var oilCompanies = await _oilCompanyService.GetAllOilCompaniesAsync();
            return Ok(oilCompanies);
        }

        // �������� �������� �� ID
        [HttpGet("{id}")]
        public async Task<ActionResult<OilCompanyDTO>> GetOilCompany(int id)
        {
            var oilCompany = await _oilCompanyService.GetOilCompanyByIdAsync(id);

            if (oilCompany == null)
            {
                return NotFound();
            }

            return Ok(oilCompany);
        }

        [HttpPost]
        public async Task<ActionResult<OilCompanyDTO>> CreateOilCompany(OilCompanyDTO oilCompanyDTO)
        {
            // ������������� �������� ��� �������� ��������
            var createdOilCompanyDTO = await _oilCompanyService.CreateOilCompanyAsync(oilCompanyDTO);

            return CreatedAtAction(nameof(GetOilCompany), new { id = createdOilCompanyDTO.id }, createdOilCompanyDTO);
        }


        // �������� ������������ �������� � � ���������� � IT
        [HttpPut("{id}")]
        public async Task<ActionResult<OilCompanyDTO>> UpdateOilCompany(int id, OilCompanyDTO oilCompanyDTO)
        {
            var updatedOilCompany = await _oilCompanyService.UpdateOilCompanyAsync(id, oilCompanyDTO);

            if (updatedOilCompany == null)
            {
                return NotFound();
            }

            return Ok(updatedOilCompany);
        }


        // ������� �������� � ��������� � ��� ���������� � IT
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOilCompany(int id)
        {
            var result = await _oilCompanyService.DeleteOilCompanyAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
