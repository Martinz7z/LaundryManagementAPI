using LaundryManagement.Api.DTOs;
using LaundryManagement.Api.Interfaces;
using LaundryManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaundryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceRepository _serviceRepository;

    public ServicesController(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
    {
        var services = await _serviceRepository.GetAllAsync();

        var serviceDtos = services.Select(service => new ServiceDto
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            Price = service.Price
        });

        return Ok(serviceDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceDto>> GetService(int id)
    {
        var service = await _serviceRepository.GetByIdAsync(id);

        if (service == null)
        {
            return NotFound();
        }

        var serviceDto = new ServiceDto
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            Price = service.Price
        };

        return Ok(serviceDto);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceDto>> CreateService(
        CreateServiceDto createServiceDto)
    {
        var service = new Service
        {
            Name = createServiceDto.Name,
            Description = createServiceDto.Description,
            Price = createServiceDto.Price
        };

        await _serviceRepository.CreateAsync(service);

        var serviceDto = new ServiceDto
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            Price = service.Price
        };

        return CreatedAtAction(
            nameof(GetService),
            new { id = service.Id },
            serviceDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateService(
        int id,
        UpdateServiceDto updateServiceDto)
    {
        var service = await _serviceRepository.GetByIdAsync(id);

        if (service == null)
        {
            return NotFound();
        }

        service.Name = updateServiceDto.Name;
        service.Description = updateServiceDto.Description;
        service.Price = updateServiceDto.Price;

        await _serviceRepository.UpdateAsync(service);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(int id)
    {
        var deleted = await _serviceRepository.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}