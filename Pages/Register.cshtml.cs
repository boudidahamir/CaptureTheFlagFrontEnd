using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;

public class RegisterModel : PageModel
{
    private readonly HttpClient _httpClient;

    public RegisterModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string ConfirmPassword { get; set; } = string.Empty;

    [BindProperty]
    public string Nickname { get; set; } = string.Empty;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Password != ConfirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Passwords do not match.");
            return Page();
        }

        var payload = new { email = Email, password = Password, nickname = Nickname };
        var response = await _httpClient.PostAsJsonAsync("http://localhost:3000/api/auth/register", payload);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("index");
        }

        ModelState.AddModelError(string.Empty, "Registration failed.");
        return Page();
    }
}
