using Blog_Application.Data;
using Blog_Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Blog_Application.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webhostEnvironment;

        private readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        // Constructor to inject the ApplicationDbContext
        public PostController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index(int? categoryId)
        {
            var postQuery = _context.Posts.Include(p => p.Category).AsQueryable();

           if(categoryId.HasValue)
           {
               postQuery = postQuery.Where(p => p.CategoryId == categoryId);
           }

            var posts = postQuery.ToList();

            ViewBag.Categories = _context.Categories.ToList();

      
             return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult>Detial(int id){
            if(id == null) { 
                return NotFound();
            }
            var post = _context.Posts.Include(p => p.Comments)
                .FirstOrDefault(p => p.Id == id);
            
            if(post == null)    
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpGet]
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
        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var inputFileExtension = Path.GetExtension(postViewModel.FeatureImage.FileName).ToLower();
                bool isAllowed = permittedExtensions.Contains(inputFileExtension);

                if (!isAllowed)
                {
                    ModelState.AddModelError("FeatureImage", "Invalid file type. Only .jpg, .jpeg, .png, and .gif are allowed.");
                    return View(postViewModel);
                }

                postViewModel.Post.FeatureImagePath = await UploadFiletoFolder(postViewModel.FeatureImage);
                await _context.Posts.AddAsync(postViewModel.Post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            postViewModel.Categories = _context.Categories.Select(c =>
               new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Name
               }
           ).ToList();

            return View(postViewModel);
        }
        public async Task<String> UploadFiletoFolder(IFormFile file)
        {
            var inputFileExtension = Path.GetExtension(file.FileName).ToLower();
            var fileName = Guid.NewGuid().ToString() + inputFileExtension;
            var wwwRootPath = _webhostEnvironment.WebRootPath;
            var imagesFolderPath = Path.Combine(wwwRootPath, "images");

            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            var filePath = Path.Combine(imagesFolderPath, fileName);
            try
            {
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);

                }
            }
            catch (Exception ex)
            {
                return $"Error uploading Images: {ex.Message}";
            }
            return "/images/" + fileName;
        }
    }
}
