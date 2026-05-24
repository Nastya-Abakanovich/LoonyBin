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
        public IActionResult GetById([FromRoute] Guid Id)
        {
            var result = patientService.GetById(Id);

            return result.Match<IActionResult>(
                patient => Ok(patient.Adapt<PatientResponse>()),
                _ => NotFound());
        }
    }
}
