using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogContext _context;

    public HomeController(ILogger<HomeController> logger, BlogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult About()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }
   

    public IActionResult Index()
    {
        var list = _context.Blog.Take(5).Where(b => b.IsPublish).OrderByDescending(x =>x.CreateTime).ToList();
        foreach (var blog in list)
        {
            blog.Author = _context.Author.Find(blog.AuthorId);
        }
        return View(list);
    }

    public IActionResult Post(int Id)
    {
        var blog = _context.Blog.Find(Id);
        if (blog == null) // Eðer Id ile eþleþen bir blog bulunmazsa
        {
            return NotFound("Blog not found."); // 404 dönebilirsiniz
        }
        blog.Author = _context.Author.Find(blog.AuthorId);

        blog.ImagePath = "/img/" + blog.ImagePath;
        return View(blog);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new Blog { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
