using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;

public class ForgotPasswordModel : PageModel
{
    private readonly HttpClient _httpClient;

    public ForgotPasswordModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var payload = new { email = Email };
        var response = await _httpClient.PostAsJsonAsync("http://localhost:3000/api/auth/recover", payload);
        if (response.IsSuccessStatusCode)
        {
            // Optionally, show a confirmation message
            return RedirectToPage("Index");
        }

        ModelState.AddModelError(string.Empty, "Error sending reset link.");
        return Page();
    }
}
