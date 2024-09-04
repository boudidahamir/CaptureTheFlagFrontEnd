using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;

public class ResetPasswordModel : PageModel
{
    private readonly HttpClient _httpClient;

    public ResetPasswordModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [BindProperty]
    public string? Token { get; set; }

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public void OnGet(string token)
    {
        Token = token;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(Token))
        {
            ModelState.AddModelError(string.Empty, "Invalid token.");
            return Page();
        }

        var payload = new { password = Password };
        var response = await _httpClient.PostAsJsonAsync($"http://localhost:3000/api/auth/reset/{Token}", payload);
        if (response.IsSuccessStatusCode)
        {
            // Optionally, show a success message or redirect to login
            return RedirectToPage("Index");
        }

        ModelState.AddModelError(string.Empty, "Error resetting password.");
        return Page();
    }
}
