using System.Text;
using UrlShortener.Core.Contract;
using UrlShortener.Model;

namespace UrlShortener.Service
{
    public class UrlService : IUrlService
    {

        private const string Alphabet = "1234567890abcdfghjkmnpqrstvwxyzABCDFGHJKLMNPQRSTVWXYZ";
        private static readonly int Base = Alphabet.Length;
        private readonly IUrlRepository _urlRepository;

        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }
        public async Task<UrlModel> Add(string orginalUrl, CancellationToken cancellationToken)
        {
            if (await _urlRepository.DoseExist(orginalUrl, cancellationToken))
                throw new Exception("This url already exists in data base");
            else
            {
                var id = await _urlRepository.Add(orginalUrl, cancellationToken);
                return new UrlModel
                {
                    ShortUrl = GenerateShortUrl(id),
                    OrginalUrl = orginalUrl,
                };
            }
        }

        public async Task<UrlModel> Get(string shortUrl, CancellationToken cancellationToken)
        {
            var url = new UrlModel
            {
                OrginalUrl = await _urlRepository.GetOrginalUrl(GetIdByShortUrl(shortUrl), cancellationToken),
                ShortUrl = shortUrl,
            };
            return url;
        }

        private static string GenerateShortUrl(int id)
        {
            var sb = new StringBuilder();
            int i = 6;
            id--;
            while (id > 0 || i > 0)
            {
                sb.Insert(0, Alphabet.ElementAt(id % Base));
                id = id / Base;
                i--;
            }
            return sb.ToString();
        }

        private static int GetIdByShortUrl(string shortUrl)
        {
            var id = 0;
            for (var i = 0; i < shortUrl.Length; i++)
            {
                id = id * Base + Alphabet.IndexOf(shortUrl.ElementAt(i));
            }
            return id + 1;
        }
    }
}
