using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OrdersWeb.Pages;

public class XssDemoModel : PageModel
{
    public string? Q { get; private set; }

    public void OnGet(string? q) => Q = q;
}
