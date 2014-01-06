using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product
{
    public interface IGpsVersionRepository
    {
        IQueryable<ProductGpsVersion> GetAll();
        Task<ProductGpsVersion> Get(byte id);
        Task<ProductGpsVersion> Add(ProductGpsVersion item);
        Task<bool> Update(ProductGpsVersion item);
        Task<bool> Remove(byte id);
    }
}
