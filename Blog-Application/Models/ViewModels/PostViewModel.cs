using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_Application.Models.ViewModels
{
    public class PostViewModel
    {
        public Post  Post{ get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IFormFile  FeatureImage{ get; set; }



    }
}
