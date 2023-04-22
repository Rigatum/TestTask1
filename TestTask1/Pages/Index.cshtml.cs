using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask1.Data;
using TestTask1.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace TestTask1.Pages;

public class IndexModel : PageModel
{
    private ApplicationContext _db;
    public List<Street> Streets { get; private set; } = new();
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, ApplicationContext db)
    {
        _logger = logger;
        _db = db;
    }

    public void OnGet()
    {
        Streets = _db.Streets.Include(u => u.City).ToList();

        foreach (var street in Streets)
            Console.WriteLine($"{street.City.CityName} - {street.StreetName}");
    }
}
