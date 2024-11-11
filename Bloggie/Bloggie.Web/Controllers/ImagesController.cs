﻿using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Net;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
            {
            // call a repository
                var imageUrl = await imageRepository.UploadAsync(file);
                if (imageUrl == null)
                {
                    return Problem("SOMETHING WENT WRONG!", null, (int)HttpStatusCode.InternalServerError);
                }
                return new JsonResult(new {link = imageUrl});
            }
    }
}
