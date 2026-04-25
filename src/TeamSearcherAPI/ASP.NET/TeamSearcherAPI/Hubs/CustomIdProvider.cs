using Microsoft.AspNetCore.SignalR;

namespace TeamSearcherAPI.Hubs;

public class CustomUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        var query = connection.GetHttpContext()?.Request.Query;

        var userId = query?["userId"].ToString();
        var teamId = query?["teamId"].ToString();

        var result = !string.IsNullOrEmpty(userId) ? userId 
                    : !string.IsNullOrEmpty(teamId) ? teamId 
                    : null;

        Console.WriteLine($"GetUserId called: userId={query?["userId"]}, teamId={query?["teamId"]}, returning={result}");
        // Returns whichever one is present
        return result;
    }
}