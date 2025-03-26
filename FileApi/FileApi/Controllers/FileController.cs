using FileApi.Models;
using FileApi.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileApi.Controllers;

[Route("Files")]
[ApiController]
public class FileController : ControllerBase
{

    private readonly IFileService _fileServie;

    
    public FileController(IFileService fileServie)
    {
        _fileServie = fileServie;

    }

    [HttpGet("dowload/{fileName}")]
    public IActionResult Download(string fileName)
    {
        return File(_fileServie.DownloadFile(fileName), "application/pdf");
    }
    
    [HttpGet]
    public IActionResult Test()
    {
        return Ok();
    }
    
    [HttpGet("upload")]
    public async Task<IActionResult> Upload(UploadFileRequest body)
    {
        await _fileServie.UploadFile(body);
        return Ok();
    }
}