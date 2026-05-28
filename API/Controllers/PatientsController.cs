using LoonyBin.API.Dtos;
using LoonyBin.Features.DateFilters;
using LoonyBin.Features.Patients;
using LoonyBin.Infrastructure.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace LoonyBin.API.Controllers
{
    [ApiController]
    [Route("patients")]
    public class PatientsController(IPatientService patientService): ControllerBase
    {
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>  GetByIdAsync([FromRoute] Guid id, 
            CancellationToken ct = default)
        {
            var result = await patientService.GetByIdAsync(id, ct);

            return result.Match<IActionResult>(
                patient => Ok(patient.Adapt<PatientResponse>()),
                _ => NotFound());
        }

        [HttpPost()]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] PatientRequest request,
            CancellationToken ct = default)
        {
            var patient = await patientService.CreateAsync(request.Adapt<Patient>(), ct);

            return Ok(patient.Adapt<PatientResponse>());
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, 
            [FromBody] PatientRequest request,
            CancellationToken ct = default)
        {
            var result = await patientService.UpdateAsync(id, request.Adapt<Patient>(), ct);

            return result.Match<IActionResult>(
                patient => Ok(patient.Adapt<PatientResponse>()),
                _ => NotFound());
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id,
            CancellationToken ct = default)
        {
            var result = await patientService.DeleteAsync(id, ct);

            return result.Match<IActionResult>(
                _ => Ok(),
                _ => NotFound());
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<PatientResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchByBirthDateAsync([FromQuery(Name = "date")] List<string>? dateParams, 
            CancellationToken ct = default)
        {
            var filters = dateParams?.Select(x => DateFilter.Parse(x)).ToList() ?? new();
            var result = await patientService.SearchByBirthDateAsync(filters, ct);

            return Ok(result.Adapt<List<PatientResponse>>());
        }
    }
}
