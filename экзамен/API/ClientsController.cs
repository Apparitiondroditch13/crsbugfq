using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreditAPI.Models;
using CreditAPI.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/clients/fined
        // Возвращает клиентов с неоплаченными штрафами
        [HttpGet("fined")]
        public async Task<ActionResult<IEnumerable<ClientFineDto>>> GetClientsWithFines()
        {
            var clients = await _context.Clients
                .Where(c => c.HasUnpaidFines)
                .Select(c => new ClientFineDto
                {
                    FullName = c.FullName,
                    BirthDate = c.BirthDate,
                    FineAmount = c.FineAmount
                })
                .ToListAsync();

            return Ok(clients);
        }

        // GET: api/clients/all
        // Возвращает всех клиентов
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ClientAllDto>>> GetAllClients()
        {
            var clients = await _context.Clients
                .Select(c => new ClientAllDto
                {
                    FullName = c.FullName,
                    BirthDate = c.BirthDate,
                    AverageSalary = c.AverageSalary
                })
                .ToListAsync();

            return Ok(clients);
        }
    }
}