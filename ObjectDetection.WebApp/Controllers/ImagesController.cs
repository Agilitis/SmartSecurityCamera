using System;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ObjectDetectionWithTrainingML.Model;

namespace ObjectDetection.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowFrontEnd")]
    public class ImagesController : ControllerBase
    {
        
        [HttpPost]
        public IActionResult SaveImage([FromBody]string base64image)
        {
            if (string.IsNullOrEmpty(base64image))
                return BadRequest();
       
            var t = base64image.Substring(22);  // remove data:image/png;base64,
       
            byte[] bytes = Convert.FromBase64String(t);
           
       
           
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            var randomFileName = Guid.NewGuid().ToString().Substring(0, 4) + ".png";
            image.Save("./" + randomFileName, System.Drawing.Imaging.ImageFormat.Png);
            var input = new ModelInput();
           
            input.ImageSource = "./" + randomFileName;
           
            ModelOutput result = ConsumeModel.Predict(input);
           
            return Ok(new { message = $"The image contains a handgun with a chance of {result.Score[1] * 100}%" });
                          
        }
    }
}