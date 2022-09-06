using Infrastructure.UrlShortener.Data;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Core.Contract;
using UrlShortener.Core.Entity;

namespace UrlShortener.Infrastructure.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly AppDbContext _dbContext;

        public UrlRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Add(string orginalUrl, CancellationToken cancellationToken)
        {
            var entity = new Url { OrginalUrl = orginalUrl };
            await _dbContext.Urls.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task<bool> DoseExist(string orginalUrl, CancellationToken cancellationToken)
        {
            var doseExist = await _dbContext.Urls.AnyAsync(u => u.OrginalUrl == orginalUrl, cancellationToken);
            return doseExist;
        }

        public async Task<string> GetOrginalUrl(int id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Urls.Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
            return entity.OrginalUrl;
        }
    }
}
