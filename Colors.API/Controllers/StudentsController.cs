using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colors.API.Controllers;

/// <summary>
/// An example controller for testing <code>multipart/form-data</code> submission
/// </summary>
[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class StudentsController(ILogger<StudentsController> logger) : ControllerBase
{

    /// <summary>
    /// View a form
    /// </summary>
    /// <param name="id">Student ID</param>
    /// <param name="formId">Form ID</param>
    /// <returns></returns>
    [HttpGet("{id:int}/forms/{formId:int}")]
    [ProducesResponseType(typeof(FormSubmissionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<FormSubmissionResult> ViewForm(int id, int formId)
    {
        logger.LogInformation("viewing the form#{formId} for Student ID={id}", formId, id);
        await Task.Delay(1000);
        return new FormSubmissionResult { FormId = formId, StudentId = id };
    }

    /// <summary>
    /// Submit a form which contains a key-value pair and a file.
    /// </summary>
    /// <param name="id">Student ID</param>
    /// <param name="form">A form which contains the FormId and a file</param>
    /// <returns></returns>
    [HttpPost("{id:int}/forms")]
    [ProducesResponseType(typeof(FormSubmissionResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FormSubmissionResult>> SubmitForm(int id, [FromForm] StudentForm form)
    {
        logger.LogInformation("validating the form#{formId} for Student ID={id}", form.FormId ,id);
        logger.LogInformation("saving file [{fileName}]", form.StudentFile.FileName);
        await Task.Delay(1500);
        logger.LogInformation("file saved.");
        var result = new FormSubmissionResult { FormId = form.FormId, StudentId = id };
        return CreatedAtAction(nameof(ViewForm), new { id, form.FormId }, result);
    }
    [HttpPost("{id:int}/forms2")]
    [ProducesResponseType(typeof(FormSubmissionResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FormSubmissionResult>> SubmitForm2(int id, [FromBody] StudentForm2 form)
    {
        logger.LogInformation("validating the form#{formId} for Student ID={id}", form.FormId, id);
        await Task.Delay(1500);
        logger.LogInformation("file saved.");
        var result = new FormSubmissionResult { FormId = form.FormId, StudentId = id };
        return CreatedAtAction(nameof(ViewForm), new { id, form.FormId }, result);
    }
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id:int}/forms/{formId:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<bool> Delete(int id, int formId)
    {
        logger.LogInformation("This API is hidden in Swagger UI and Swagger JSON doc.");
        logger.LogInformation("deleting the form#{formId} for student ID=[{id}]", formId, id);
        await Task.Delay(1500);
        return true;
    }

    /// <summary>
    /// Get students by residential type
    /// </summary>
    /// <param name="residentialType">Residential Type. **Default**: <code>InState</code>.</param>
    /// <returns></returns>
    [HttpGet("")]
    public ActionResult GetStudentsByResidentialType(ResidentialType residentialType = ResidentialType.InState)
    {
        logger.LogInformation("query {residentialType} students", residentialType);
        if (residentialType == ResidentialType.International)
        {
            logger.LogInformation("found 10000 students.");
        }
        return Ok();
    }
}

public class StudentForm
{
    [Required] public int FormId { get; set; }
    [Required] public IFormFile StudentFile { get; set; } = null!;
    [Required] public DateTime SignatureDateTime { get; set; }
}

public class StudentForm2
{
    [Required] public int FormId { get; set; }
    [Required] public DateTime SignatureDateTime { get; set; }
}
public class FormSubmissionResult
{
    public int StudentId { get; set; }
    public int FormId { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ResidentialType
{
    InState,
    OutOfState,
    International
}