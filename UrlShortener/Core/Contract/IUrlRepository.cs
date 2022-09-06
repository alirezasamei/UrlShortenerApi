namespace UrlShortener.Core.Contract
{
    public interface IUrlRepository
    {
        public Task<int> Add(string orginalUrl, CancellationToken cancellationToken);
        public Task<string> GetOrginalUrl(int id, CancellationToken cancellationToken);
        public Task<bool> DoseExist(string orginalUrl, CancellationToken cancellationToken);
    }
}
