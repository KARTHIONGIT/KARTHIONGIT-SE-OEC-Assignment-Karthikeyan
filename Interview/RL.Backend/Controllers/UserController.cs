using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Commands;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly RLContext _context;
    private readonly IMediator _mediator;


    public UsersController(ILogger<UsersController> logger, RLContext context, IMediator mediator)
    {
        _logger = logger;
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [EnableQuery]
    public IEnumerable<User> Get()
    {
        return _context.Users;
    }

    [HttpGet]
    [Route("GetProcedureUsers")]
    public IEnumerable<ProcedureUser> GetProcedureUsers(int planId)
    {
        return _context.ProcedureUsers.Where(x => x.PlanId == planId);
    }

    [HttpPost("AddProcedureUsers")]
    public async Task<IActionResult> AddProcedureUsers(AddProcedureUserCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);

        return response.ToActionResult();
    }

    [HttpDelete("RemoveProcedureUsers/{planId}/{procedureId}")]
    public async Task<IActionResult> DeleteProcedureUsers(int planId, int procedureId)
    {
        if (_context.ProcedureUsers == null)
        {
            return NoContent();
        }
        var entitiesToDelete = await _context.ProcedureUsers
        .Where(pu => pu.PlanId == planId && pu.ProcedureId == procedureId)
        .ToListAsync();

        if (entitiesToDelete == null || !entitiesToDelete.Any())
        {
            return NotFound();
        }
        _context.ProcedureUsers.RemoveRange(entitiesToDelete);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
