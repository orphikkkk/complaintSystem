using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SajhaSabal.Models;

namespace SajhaSabal.Controllers;
using SajhaSabal.Models;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SsdbContext _context;
    public HomeController(ILogger<HomeController> logger,SsdbContext ssdbContext)
    {
        _logger = logger;
        _context = ssdbContext;
    }

    public IActionResult Index()
    {
        List<NoticeModel> notices = _context.Notices.ToList();
        List<ComplaintModel> complaints = _context.Complaints.ToList();

        ViewBag.Complaints = complaints;
        return View(notices);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}