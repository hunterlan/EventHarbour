using EventHarbour.EventService.Presentation.DTOs;

namespace EventHarbour.EventService.Presentation.Clients;

public class UserServiceClient
{
    private readonly HttpClient _httpClient;

    public UserServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        //TODO: Move to config file? 
        _httpClient.BaseAddress = new Uri("https://localhost:7080");
        
        //TODO: Add headers that shows that request come from this service
    }

    public async Task<string?> GetOrganizerFullname(int organizerId, CancellationToken token)
    {
        try
        {
            await GetUserAsync(organizerId, token);
            return "John Doe";
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> IsOrganizerExist(int organizerId, CancellationToken token)
    {
        try
        {
            var organizer = await GetUserAsync(organizerId, token);
            return organizer is not null;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private async Task<UserDto?> GetUserAsync(int organizerId, CancellationToken token)
    {
        return await _httpClient.GetFromJsonAsync<UserDto>($"users/{organizerId}", cancellationToken: token);
    }
}