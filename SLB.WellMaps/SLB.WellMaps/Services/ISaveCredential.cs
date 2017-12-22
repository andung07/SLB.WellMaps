
namespace SLB.WellMaps.Services
{
    public interface ISaveCredential
    {
        bool Save(string URL, string userName, string password);
    }
}
