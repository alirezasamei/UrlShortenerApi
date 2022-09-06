using UrlShortener.Model;

namespace UrlShortener.Core.Contract
{
    public interface IUrlService
    {
        public Task<UrlModel> Add(string orginalUrl, CancellationToken cancellationToken);
        public Task<UrlModel> Get(string shortUrl, CancellationToken cancellationToken);
    }
}
