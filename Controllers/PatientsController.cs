using LoonyBin.DAL;
using LoonyBin.Services;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace LoonyBin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientsController(IPatientService patientService) : ControllerBase
    {
        [HttpGet("/patients/{Id:Guid}", Name = "GetById")]
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

        [HttpPost("/patients", Name = "Create")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateAsync([FromBody] PatientRequest request,
            CancellationToken ct = default)
        {
            var result = await patientService.CreateAsync(request.Adapt<Patient>(), ct);

            return result.Match<IActionResult>(
                patient => Ok(patient.Adapt<PatientResponse>()),
                _ => Conflict());
        }

        [HttpPut("/patients", Name = "Update")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] PatientRequest request,
            CancellationToken ct = default)
        {
            var result = await patientService.UpdateAsync(request.Adapt<Patient>(), ct);

            return result.Match<IActionResult>(
                patient => Ok(patient.Adapt<PatientResponse>()),
                _ => NotFound());
        }

        [HttpDelete("/patients/{Id:Guid}", Name = "Delete")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id,
            CancellationToken ct = default)
        {
            var result = await patientService.DeleteAsync(id, ct);

            return result.Match<IActionResult>(
                _ => Ok(),
                _ => NotFound());
        }
    }
}
