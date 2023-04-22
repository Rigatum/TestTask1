using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask1.Services;
using Dadata.Model;
using TestTask1.Contracts;
namespace TestTask1.Pages;

public class ManageObjectsModel : PageModel
{
    private readonly ILogger<ManageObjectsModel> _logger;
    

    public ManageObjectsModel(ILogger<ManageObjectsModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
        
    }
}

