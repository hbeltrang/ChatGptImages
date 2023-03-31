using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI;

namespace ChatGptImages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateImagesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GenerateImagesController(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        [HttpGet]
        public async Task<IActionResult> GenerateImages(string title, int qty)
        {
            List<string> imagesList = new List<string>();

            var token = _configuration.GetValue<string>("ChatGpt:Token");

            var openAIClient = new OpenAIClient(new OpenAIAuthentication(token));
            var imageResult = await openAIClient.ImagesEndPoint.GenerateImageAsync(title, qty, OpenAI.Images.ImageSize.Medium);
            foreach (var image in imageResult)
            {
                imagesList.Add(image);
            }

            return Ok(imagesList);
        }
    }
}
