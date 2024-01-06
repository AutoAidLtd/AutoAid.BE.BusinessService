using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAid.Application.Service
{
    public interface IPlaceService : IDisposable
    {
        Task<bool> Create(double lat, double lng);
    }
}
