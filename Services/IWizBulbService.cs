using WizBulbApi.Models;

namespace WizBulbApi.Services;

public interface IWizBulbService
{
    Task<string> DiscoverBulbsAsync();
    Task<string> SetPilotAsync(string ipAddress, WizBulbPilot pilot);
    Task<string> GetPilotAsync(string ipAddress);
    Task<string> SetSystemConfigAsync(string ipAddress, WizBulbConfig config);
}