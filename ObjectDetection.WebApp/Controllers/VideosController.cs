using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectDetectionWithTrainingML.Model;

namespace ObjectDetection.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowFrontEnd")]
    public class VideosController : ControllerBase
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

                    var input = new ModelInput();

                    input.ImageSource = path;

                    ModelOutput result = ConsumePictureModel.Predict(input);

                    var highestScore = 0.0;

                    foreach (var score in result.Score)
                    {
                        if (highestScore < score)
                        {
                            highestScore = score;
                        }
                    }

                    return Ok(new { message = $"The image contains a handgun with a chance of {result.Score[4] * 100}%" });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}