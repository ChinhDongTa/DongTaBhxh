using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace DongTaBhxh.WebUI.Data;

public static class AuthProviderHelper {

    public static async Task<string> GetUserIdAsync(this AuthenticationStateProvider authProvider)
        => (await authProvider.GetAuthenticationStateAsync()).User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
}