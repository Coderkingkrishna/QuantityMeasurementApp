using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Models.DTOs;
using QuantityMeasurementApp.Models.Entities;
using QuantityMeasurementApp.Repository;

namespace QuantityMeasurementApp.HistoryService.Controllers;

[ApiController]
[Route("api/history")]
public class OperationHistoryController : ControllerBase
{
    private readonly IQuantityMeasurementRepository _repository;

    public OperationHistoryController(IQuantityMeasurementRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<OperationHistoryDTO>> GetAll()
    {
        var items = _repository
            .GetAll()
            .Select(MapToDto)
            .ToList();

        return Ok(items);
    }

    [HttpGet("user/{userId:guid}")]
    public ActionResult<IReadOnlyList<OperationHistoryDTO>> GetByUser(Guid userId)
    {
        var items = _repository
            .GetByUserId(userId)
            .Select(MapToDto)
            .ToList();

        return Ok(items);
    }

    [HttpPost]
    public ActionResult<OperationHistoryDTO> Create([FromBody] CreateOperationHistoryRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var entity = request.IsError
            ? new QuantityMeasurementEntity(request.Description, request.ErrorMessage ?? string.Empty, request.UserId)
            : new QuantityMeasurementEntity(request.Description, request.UserId);

        _repository.Save(entity);

        return CreatedAtAction(
            nameof(GetAll),
            new OperationHistoryDTO
            {
                CreatedAt = entity.CreatedAt,
                Description = entity.Description,
                IsError = entity.IsError,
                ErrorMessage = entity.ErrorMessage,
            }
        );
    }

    private static OperationHistoryDTO MapToDto(QuantityMeasurementEntity entity)
    {
        return new OperationHistoryDTO
        {
            CreatedAt = entity.CreatedAt,
            Description = entity.Description,
            IsError = entity.IsError,
            ErrorMessage = entity.ErrorMessage,
        };
    }
}

public sealed class CreateOperationHistoryRequest
{
    [Required]
    [MaxLength(1000)]
    public string Description { get; init; } = string.Empty;

    public bool IsError { get; init; }

    [MaxLength(1000)]
    public string? ErrorMessage { get; init; }

    public Guid? UserId { get; init; }
}
