using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIM.Domain.Models.Item;

namespace SIM.Domain.Models.Invoice
{
    public class InvoiceRow
    {
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public long Fee { get; set; }
        public int DiscountPercent { get; set; }
        public long FeeAfterDiscount { get; set; }
        public long TotalPriceBeforeDiscount { get; set; }
        public long PayablePrice { get; set; }
        public long FeeDiscountPrice { get; set; }
        public long TotalDiscountPrice { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Item.Item Item { get; set; }
    }
}
