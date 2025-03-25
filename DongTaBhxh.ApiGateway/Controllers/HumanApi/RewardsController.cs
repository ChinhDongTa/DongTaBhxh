using DongTa.ResponseResult;
using DongTaBhxh.DataTranfer.Mapping;
using DongTaBhxh.Domain.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DongTaBhxh.ApiGateway.Controllers.HumanApi {

    [Route("[controller]")]
    [ApiController]
    public class RewardsController(DongTaBhxhDbContext context) : ControllerBase {

        // GET: api/Rewards
        [HttpGet("All")]
        public async Task<IActionResult> All()
        {
            return Ok(ResultExtension.GetResult(
                await context.Rewards.Select(x => x.ToDto())
                .ToListAsync()));
        }

        // GET: Rewards/GetOne/5
        [HttpGet("GetOne/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var reward = await context.Rewards.FindAsync(id);
            return Ok(ResultExtension.GetResult(reward?.ToDto()));
        }
    }
}