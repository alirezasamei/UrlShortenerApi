using Microsoft.AspNetCore.Mvc;
using UrlShortener.Core.Contract;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlShortenerController : ControllerBase
    {

        private readonly ILogger<UrlShortenerController> _logger;
        private readonly IUrlService _urlService;

        public UrlShortenerController(ILogger<UrlShortenerController> logger,
            IUrlService urlService)
        {
            _logger = logger;
            _urlService = urlService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Create(string OrginalUrl, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var url = await _urlService.Add(OrginalUrl, cancellationToken);
                    _logger.LogInformation("Short url: {shortUrl} was created for orginal url: {orginalUrl}", url.ShortUrl, url.OrginalUrl);
                    return Ok(url.ShortUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Get(string shortUrl, CancellationToken cancellationToken)
        {
            try
            {
                var orginalUrl = (await _urlService.Get(shortUrl, cancellationToken)).OrginalUrl;
                return Ok(orginalUrl);
            }
            catch (NullReferenceException ex)
            {
                ModelState.AddModelError(String.Empty, $"this short url is not defined ,' {ex.Message} '");
            }
            return BadRequest(ModelState);
        }
    }
}