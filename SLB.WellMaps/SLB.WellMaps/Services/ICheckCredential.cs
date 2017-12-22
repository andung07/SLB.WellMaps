using SLB.WellMaps.Models;
using System.Threading.Tasks;

namespace SLB.WellMaps.Services
{
    public interface ICheckCredential
    {
        Credential Check();
    }
}
