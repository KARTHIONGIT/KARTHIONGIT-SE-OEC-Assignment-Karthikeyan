using MediatR;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Commands.Handlers.Plans
{
    public class AddProcedureUserHandler : IRequestHandler<AddProcedureUserCommand, ApiResponse<Unit>>
    {
        private readonly RLContext _context;

        public AddProcedureUserHandler(RLContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<Unit>> Handle(AddProcedureUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var procUsers = new ProcedureUser()
                {
                    PlanId = request.PlanId,
                    ProcedureId = request.ProcedureId,
                    UserId = request.UserId,
                };
                _context.ProcedureUsers.Add(procUsers);

                await _context.SaveChangesAsync();

                return ApiResponse<Unit>.Succeed(new Unit());
            }
            catch (Exception e)
            {
                return ApiResponse<Unit>.Fail(e);
            }
        }
    }
}