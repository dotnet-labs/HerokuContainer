namespace Colors.API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "v2")]
[Produces("application/json")]
[Route("api/[controller]")]
public class FilesController(ILogger<FilesController> logger) : ControllerBase
{
    /// <summary>
    /// Download a file. This demo will generate a txt file.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "Download a File by FileID")]
    public IActionResult Download(int id)
    {
        return File("hello world"u8.ToArray(), "text/plain", $"test-{id}.txt");
    }

    /// <summary>
    /// Upload a file. This demo is dummy and only waits 2 seconds.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost("single-file")]
    public async Task Upload(IFormFile file)
    {
        logger.LogInformation("validating the file {fileName}", file.FileName);
        logger.LogInformation("saving file");
        await Task.Delay(2000); // validate file type/format/size, scan virus, save it to a storage
        logger.LogInformation("file saved.");
    }

    /// <summary>
    /// Upload two files. This demo is dummy and only waits 2 seconds.
    /// </summary>
    /// <param name="file1"></param>
    /// <param name="file2"></param>
    /// <returns></returns>
    [HttpPost("two-files")]
    public async Task Upload(IFormFile file1, IFormFile file2)
    {
        logger.LogInformation("validating the file {fileName}", file1.FileName);
        logger.LogInformation("validating the file {fileName}", file2.FileName);
        logger.LogInformation("saving files");
        await Task.Delay(2000);
        logger.LogInformation("files saved.");
    }

    /// <summary>
    /// Upload multiple files. This demo is dummy and only waits 2 seconds.
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    [HttpPost("multiple-files")]
    public async Task Upload(List<IFormFile> files)
    {
        logger.LogInformation("validating {n} files", files.Count);
        foreach (var file in files)
        {
            logger.LogInformation("saving file {fileName}", file.FileName);
            await Task.Delay(1000);
        }
        logger.LogInformation("All files saved.");
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("deleting file ID=[{id}]", id);
        await Task.Delay(1500);
        return true;
    }
}