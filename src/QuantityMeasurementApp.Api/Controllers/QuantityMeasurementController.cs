using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Business;
using QuantityMeasurementApp.Models.DTOs;

namespace QuantityMeasurementApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuantityMeasurementController : ControllerBase
{
    private readonly IQuantityMeasurementService _service;

    public QuantityMeasurementController(IQuantityMeasurementService service)
    {
        _service = service;
    }

    [HttpPost("convert")]
    public ActionResult<QuantityDTO> Convert([FromBody] ConvertRequestDTO request)
    {
        try
        {
            var result = _service.Convert(request.Source, request.TargetUnit);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("compare")]
    public ActionResult<bool> Compare([FromBody] CompareRequestDTO request)
    {
        try
        {
            var result = _service.Compare(request.First, request.Second);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("add")]
    public ActionResult<QuantityDTO> Add([FromBody] AddRequestDTO request)
    {
        try
        {
            var result = _service.Add(request.First, request.Second, request.TargetUnit);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("subtract")]
    public ActionResult<QuantityDTO> Subtract([FromBody] SubtractRequestDTO request)
    {
        try
        {
            var result = _service.Subtract(request.First, request.Second, request.TargetUnit);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("divide")]
    public ActionResult<double> Divide([FromBody] DivideRequestDTO request)
    {
        try
        {
            var result = _service.Divide(request.First, request.Second);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
