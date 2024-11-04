using VozAtiva.Domain.Entities;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Infrastructure.Context;

namespace VozAtiva.Infrastructure.Repository;

public class ImageRepository(AppDbContext context) : Repository<Image>(context), IImageRepository
{
}
