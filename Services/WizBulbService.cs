using System.Net.Sockets;
using System.Net;
using System.Text;
using WizBulbApi.Models;

namespace WizBulbApi.Services;

public class WizBulbService : IWizBulbService
{
    private const int Port = 38899;

    public async Task<string> DiscoverBulbsAsync()
    {
        string discoveryMessage = "{\"method\":\"getSystemConfig\"}";
        return await SendUdpMessageAsync(IPAddress.Broadcast.ToString(), discoveryMessage);
    }

    public async Task<string> SetPilotAsync(string ipAddress, WizBulbPilot pilot)
    {
        string pilotMessage = $"{{\"method\":\"setPilot\",\"params\":{{\"state\":{pilot.State.ToString().ToLower()},\"dimming\":{pilot.Dimming},\"r\":{pilot.R},\"g\":{pilot.G},\"b\":{pilot.B}}}}}";
        return await SendUdpMessageAsync(ipAddress, pilotMessage);
    }

    public async Task<string> GetPilotAsync(string ipAddress)
    {
        string pilotMessage = "{\"method\":\"getPilot\"}";
        return await SendUdpMessageAsync(ipAddress, pilotMessage);
    }

    public async Task<string> SetSystemConfigAsync(string ipAddress, WizBulbConfig config)
    {
        string configMessage = $"{{\"method\":\"setSystemConfig\",\"params\":{{\"moduleName\":\"{config.ModuleName}\",\"homeId\":{config.HomeId},\"roomId\":{config.RoomId},\"groupId\":{config.GroupId}}}}}";
        return await SendUdpMessageAsync(ipAddress, configMessage);
    }

    private async Task<string> SendUdpMessageAsync(string ipAddress, string message)
    {
        using (var udpClient = new UdpClient())
        {
            udpClient.EnableBroadcast = true;
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            var endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), Port);
            await udpClient.SendAsync(messageBytes, messageBytes.Length, endPoint);

            var receiveResult = await udpClient.ReceiveAsync();
            return Encoding.UTF8.GetString(receiveResult.Buffer);
        }
    }
}