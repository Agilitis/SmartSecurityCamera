using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace ObjectDetection.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureCognitiveServicesController : ControllerBase
    {
        const string endpoint = "https://westeurope.api.cognitive.microsoft.com/";
        const string subscriptionKey = "c8130c0b44384208a4a4e868b6ed3a68";
        public ComputerVisionClient computerVisionClient { get; set; } = Authenticate(endpoint, subscriptionKey);


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

                    List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
{
  VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
  VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
  VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
  VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
  VisualFeatureTypes.Objects
};

                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        ImageAnalysis results = await computerVisionClient.AnalyzeImageInStreamAsync(stream, features);
                        return Ok(new { message = results});

                    }



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

        /*
         * AUTHENTICATE
         * Creates a Computer Vision client used by each example.
         */
        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }
    }
}






