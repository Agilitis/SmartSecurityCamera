using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Alturos.Yolo;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectDetectionWithTrainingML.Model;

namespace ObjectDetection.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowFrontEnd")]
    public class YoloController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostFile(List<IFormFile> files)
        {
            try
            {
                var file = Request.Form.Files[0];


                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var path = "./" + fileName;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var configurationDetector = new ConfigurationDetector();
                    using (var yoloWrapper = new YoloWrapper("./yolov2-tiny-voc.cfg", "./yolov2-tiny-voc.weights", "./voc.names"))
                    {
                        var items = yoloWrapper.Detect(path);
                        Console.WriteLine((items));
                        return Ok(new { message = $"The image contains: {items}" });

                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}