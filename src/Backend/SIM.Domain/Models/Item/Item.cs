﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIM.Domain.Models.Invoice;

namespace SIM.Domain.Models.Item
{
    public class Item
    {
        public Item()
        {
        }

        public long Id { get; set; }
        public string Name { get; set; }        
        public long Fee { get; set; }

    }
}
