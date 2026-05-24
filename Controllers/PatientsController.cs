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
        public async Task<IActionResult>  GetByIdAsync([FromRoute] Guid Id)
        {
            var result = await patientService.GetByIdAsync(Id);

            return result.Match<IActionResult>(
                patient => Ok(patient.Adapt<PatientResponse>()),
                _ => NotFound());
        }

        [HttpPost("/patients", Name = "Create")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateAsync([FromBody] PatientRequest request)
        {
            var result = await patientService.CreateAsync(request.Adapt<Patient>());

            return result.Match<IActionResult>(
                patient => Ok(patient.Adapt<PatientResponse>()),
                _ => Conflict());
        }

        [HttpPut("/patients", Name = "Update")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] PatientRequest request)
        {
            var result = await patientService.UpdateAsync(request.Adapt<Patient>());

            return result.Match<IActionResult>(
                patient => Ok(patient.Adapt<PatientResponse>()),
                _ => NotFound());
        }
    }
}
