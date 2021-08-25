using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIM.Domain.Models.Item;
using SIM.Domain.Models.Invoice;


namespace SIM.Application.Interfaces
{
    public interface ISIMContext
    {
        DbSet<Item> Items { get; set; }
        DbSet<Invoice> Invoices { get; set; }

        Task SaveAsync(CancellationToken cancellationToken);
        Task CloseConnection();
    }
}
