using Blog_Application.Data;
using Blog_Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_Application.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the ApplicationDbContext
        public PostController(ApplicationDbContext context)
        {
         _context = context;   
        }
        public IActionResult Create()
        {
            var postViewModel = new PostViewModel();
            postViewModel.Categories = _context.Categories.Select(c =>
                new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }
            ).ToList();
            return View(postViewModel);
        }
    }
}
