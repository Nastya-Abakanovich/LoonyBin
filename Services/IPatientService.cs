using LoonyBin.DAL;
using OneOf;
using OneOf.Types;

namespace LoonyBin.Services
{
    public interface IPatientService
    {
        public OneOf<Patient, NotFound> GetById(Guid Id);
    }
}
