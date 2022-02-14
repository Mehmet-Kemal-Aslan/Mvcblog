using Microsoft.AspNetCore.Mvc;
using Mvcblog.Models;
using System.Diagnostics;


namespace Mvcblog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcBlog.Models.BlogContext _context;

        public HomeController(ILogger<HomeController> logger, MvcBlog.Models.BlogContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Blog.Take(5).Where(y => y.IsPublish).OrderByDescending(x => x.CreateTime).ToList();
            foreach (var blog in list)
            {
                blog.author = _context.Author.Find(blog.AuthorId);
            }
            return View(list);
        }

        public IActionResult Post(int Id)
        {
            var blog = _context.Blog.Find(Id);
            blog.author = _context.Author.Find(blog.AuthorId);
            blog.ImagePath = "/assets/img/" + blog.ImagePath;
            return View(blog);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}