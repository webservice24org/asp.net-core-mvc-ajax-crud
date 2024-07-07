using AspNetCoreAjaxCrud.Models.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAjaxCrud.DAL
{
    public class AspAjaxCrudContext : DbContext
    {
        public AspAjaxCrudContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Product>Products { get; set; }
    }
}
