using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask1.Data;
using TestTask1.Models;
using Microsoft.EntityFrameworkCore;

namespace TestTask1.Pages;

public class IndexModel : PageModel
{
    ApplicationContext context;
    public List<City> Cities { get; private set; } = new();
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, ApplicationContext db)
    {
        _logger = logger;
        context = db;
    }

    public void OnGet()
    {
        Cities = context.Cities.Take(10).ToList();
    }
}
