using EShop.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Readers
{
    public class ShipmentReader : Reader<Shipment>, IShipmentReader
    {
        public ShipmentReader(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override Shipment Get(int id)
        {
            return DataSet
                .Include(x => x.Items)
                .FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Shipment> ByNumber (string number)
        {
            if (!string.IsNullOrWhiteSpace(number))
                return GetAll()
                    .Where(x => x.Number.Contains(number));
            return GetAll();
        }
    }
}
