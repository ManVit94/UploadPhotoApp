using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UploadPhotoApp.UI.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public PhotoController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);

            var newFileName = $"{ Guid.NewGuid()}{fileExtension}";

            var filePath = Path.Combine(_environment.WebRootPath, "demoImages") + $@"\{newFileName}";

            using (var fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}