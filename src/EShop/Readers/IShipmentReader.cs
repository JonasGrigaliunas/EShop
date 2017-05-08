using EShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Readers
{
    public interface IShipmentReader : IReader<Shipment>
    {
        IQueryable<Shipment> ByNumber(string number);
    }
}
