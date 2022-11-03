using InOut.API.Builders;
using InOut.Domain.Exceptions;
using InOut.Domain.Models.Branch;
using InOut.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpPost]
        [Route("/api/v1/branches/registerBranch")]
        public async Task<IActionResult> CreateBranch(BranchModel branchModel)
        {
            try
            {
                var branchModelCreated = await _branchService.CreateBranch(branchModel);

                return Ok(new ResponseModelBuilder().WithMessage("Branch created!")
                                                    .WithSuccess(true)
                                                    .WithData(branchModelCreated)
                                                    .Build());
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .Build());
            }
        }
    }
}