using AllTheBeans.Application.Beans.Commands;
using AllTheBeans.Application.Beans.Queries;
using AllTheBeans.Core.Common;
using AllTheBeans.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AllTheBeans.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeansController : ControllerBase
{
    private readonly IMediator _mediator;

    public BeansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBean([FromBody] CreateBeanCommand command)
    {
        var result = await _mediator.Send(command);

        switch (result.Status)
        {
            case ResultStatus.Success:
                return CreatedAtAction(nameof(CreateBean), new { id = result.Value }, result.Value);

            case ResultStatus.Failure:
                return BadRequest(result.ErrorMessage);

            case ResultStatus.Error:
                return StatusCode(500, result.ErrorMessage);

            default:
                return StatusCode(500, "An unknown error occurred.");
        }

    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllBeansQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var bean = await _mediator.Send(new GetBeanByIdQuery(id));

        if (bean == null)
            return NotFound();

        return Ok(bean);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateBeanCommand command)
    {
        if (id != command.Id)
            return BadRequest("Mismatched ID");

        var result = await _mediator.Send(command);

        switch (result.Status)
        {
            case ResultStatus.Success:
                return Ok(result.Value);

            case ResultStatus.Failure:
                return BadRequest(result.ErrorMessage);

            case ResultStatus.Error:
                return StatusCode(500, result.ErrorMessage);

            default:
                return StatusCode(500, "An unknown error occurred.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteBeanCommand(id));

        switch (result.Status)
        {
            case ResultStatus.Success:
                return Ok(result.Value);

            case ResultStatus.Failure:
                return BadRequest(result.ErrorMessage);

            case ResultStatus.Error:
                return StatusCode(500, result.ErrorMessage);

            default:
                return StatusCode(500, "An unknown error occurred.");
        }
    }

    [HttpGet("bean-of-the-day")]
    public async Task<IActionResult> GetBeanOfTheDay()
    {
        var result = await _mediator.Send(new GetBeanOfTheDayQuery());

        if (result == null)
            return NotFound("A Bean of the Day could not be found");

        return Ok(result);
    }
}
