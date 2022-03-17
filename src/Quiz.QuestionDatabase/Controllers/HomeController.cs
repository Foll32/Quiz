using System.Diagnostics;
using Core.Question.Statement;
using Core.Question.Types;
using Microsoft.AspNetCore.Mvc;
using Quiz.QuestionDatabase.DB;
using Quiz.QuestionDatabase.Models;

namespace Quiz.QuestionDatabase.Controllers;

public class HomeController : Controller
{
	private readonly QuestionContext _context;
	private readonly ILogger<HomeController> _logger;

	public HomeController(QuestionContext context, ILogger<HomeController> logger)
	{
		_context = context;
		_logger = logger;
	}

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
	}
}