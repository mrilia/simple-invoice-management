using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Domain.Models.Invoice
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceRows = new HashSet<InvoiceRow>();
        }

        public long Id { get; set; }
        public string CreatedOn { get; set; }
        public string BuyerName { get; set; }
        public long Number { get; set; }
        public string Description { get; set; }
        public long TotalPrice { get; set; }
        public long TotalDiscount { get; set; }
        public long PayablePrice { get; set; }

        public virtual ICollection<InvoiceRow> InvoiceRows { get; }
    }
}
