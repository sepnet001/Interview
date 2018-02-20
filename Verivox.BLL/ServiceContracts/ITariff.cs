using System.Collections.Generic;
using Verivox.BLL.Models;

namespace Verivox.BLL.ServiceContracts
{
    public interface ITariff
    {
        Product Calculate(double consumption);

        IEnumerable<Product> Calculate(IEnumerable<double> consumptions);
    }
}
