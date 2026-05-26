using LoonyBin.DAL;
using LoonyBin.Services;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using LoonyBin.Dtos;

namespace LoonyBin.Controllers
{
    [ApiController]
    [Route("patients")]
    public class PatientsController(IPatientService patientService) : ControllerBase
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateAsync([FromBody] PatientCreateRequest request,
            CancellationToken ct = default)
        {
            var result = await patientService.CreateAsync(request.Adapt<Patient>(), ct);

            return result.Match<IActionResult>(
                patient => Ok(patient.Adapt<PatientResponse>()),
                _ => Conflict());
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, 
            [FromBody] PatientUpdateRequest request,
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
    }
}
