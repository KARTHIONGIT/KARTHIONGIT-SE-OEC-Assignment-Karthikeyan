using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using RL.Backend.Commands;
using RL.Backend.Commands.Handlers.Plans;
using RL.Backend.Exceptions;
using RL.Data;

namespace RL.Backend.UnitTests
{

    [TestClass]
    public class AddProcedureUsersTests
    {
        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(int.MinValue)]
        public async Task AddProcedureToPlanTests_InvalidPlanId_ReturnsBadRequest(int planId)
        {
            //Given
            var context = new Mock<RLContext>();
            var sut = new AddProcedureUserHandler(context.Object);
            var request = new AddProcedureUserCommand()
            {
                PlanId = planId,
                ProcedureId = 1,
                UserId = 1
            };
            //When
            var result = await sut.Handle(request, new CancellationToken());

            //Then
            result.Exception.Should().BeOfType(typeof(BadRequestException));
            result.Succeeded.Should().BeFalse();
        }
        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(int.MinValue)]
        public async Task AddProcedureToPlanTests_InvalidProcedureId_ReturnsBadRequest(int procedureId)
        {
            //Given
            var context = new Mock<RLContext>();
            var sut = new AddProcedureUserHandler(context.Object);
            var request = new AddProcedureUserCommand()
            {
                PlanId = 1,
                ProcedureId = procedureId,
                UserId = 1
            };
            //When
            var result = await sut.Handle(request, new CancellationToken());

            //Then
            result.Exception.Should().BeOfType(typeof(BadRequestException));
            result.Succeeded.Should().BeFalse();
        }
        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(int.MinValue)]
        public async Task AddProcedureToPlanTests_InvalidUserId_ReturnsBadRequest(int userId)
        {
            //Given
            var context = new Mock<RLContext>();
            var sut = new AddProcedureUserHandler(context.Object);
            var request = new AddProcedureUserCommand()
            {
                PlanId = 2,
                ProcedureId = 1,
                UserId = userId
            };
            //When
            var result = await sut.Handle(request, new CancellationToken());

            //Then
            result.Exception.Should().BeOfType(typeof(BadRequestException));
            result.Succeeded.Should().BeFalse();
        }
    }
}
