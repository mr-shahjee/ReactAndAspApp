using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace ProductBoxAssessment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly string _connectionString = "UseDevelopmentStorage=true";

        private readonly string _containerName = "cms-files-public";

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File nahi mili");

            // Azurite se connect karo
            var blobServiceClient = new BlobServiceClient(_connectionString);

            // Container banao agar exist nahi karta
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            // File upload karo
            var blobClient = containerClient.GetBlobClient(file.FileName);
            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);

            return Ok(new
            {
                message = "File upload ho gayi!",
                fileName = file.FileName,
                url = blobClient.Uri.ToString()
            });
        }
    }
}