using Microsoft.AspNetCore.Mvc;
using VozAtiva.Application.Services.Interfaces;
using VozAtiva.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace VozAtiva.API.Controllers;

[Route("[controller]")]
[ApiController]
[Produces("application/json")]
public class GeolocationController(IAlertService alertService) : ControllerBase
{

    [HttpGet("GetByCoordinateRangeAroundPoint/{latitude : double}/{longitude : double}/{latRange : double}/{longRange : double}")]
    public async Task<ActionResult<IEnumerable<AlertDTO>>> GetAlertsInCoordinateRangeAroundPoint(double latitude, double longitude, double latRange, double longRange)
    {
        var alerts = await alertService.GetByCoordinateRangeAroundPoint(latitude, longitude, latRange, longRange);
        if (!alerts.Any()) return NotFound("No alerts were found in the specified range");
        return Ok(alerts);
    }

    [HttpGet("GetByCoordinateRange/{latMin : double}/{latMax : double}/{longMin : double}/{longMax : double}")]
    public async Task<ActionResult<IEnumerable<AlertDTO>>> GetAlertsInCoordinateRange(double latitude, double longitude, double latRange, double longRange)
    {
        var alerts = await alertService.GetByCoordinateRange(latitude, longitude, latRange, longRange);
        if (!alerts.Any()) return NotFound("No alerts were found in the specified range");
        return Ok(alerts);
    }

}