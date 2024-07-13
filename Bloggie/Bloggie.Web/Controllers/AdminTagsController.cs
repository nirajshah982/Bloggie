using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;

        public AdminTagsController(BloggieDbContext bloggieDbContext )
        {
            this.bloggieDbContext = bloggieDbContext;
            //this.bloggieDbContext = bloggieDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            var tags = bloggieDbContext.Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        [ActionName("Edit")]
        [Route("admin/tags/edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var tag = bloggieDbContext.Tags.FirstOrDefault(x => x.Id == id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }

            return View(null);
        }

    }
}
