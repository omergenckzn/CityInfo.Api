﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.Api.Controllers
{

    [Microsoft.AspNetCore.Mvc.Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {


        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            var pathToFile = "test.png";

            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            } if(!_fileExtensionContentTypeProvider.TryGetContentType(pathToFile,out var contentType))
            {
                contentType = "application/octet-stream";
            }
     
                var bytes = System.IO.File.ReadAllBytes(pathToFile);
                return File(bytes, contentType, Path.GetFileName(pathToFile));
            

        }

    }
}
