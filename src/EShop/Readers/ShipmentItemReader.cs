using EShop.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Readers
{
    public class ShipmentItemReader : Reader<ShipmentItem>
    {
        public ShipmentItemReader(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override ShipmentItem Get(int id)
        {
            return DataSet
                .Include(x => x.Shipment)
                .Include(x => x.Item)
                .FirstOrDefault(x => x.Id == id);
        }
        public override IQueryable<ShipmentItem> GetAll()
        {
            return base
                .GetAll()
                .Include(x => x.Item);
        }
    }
}

