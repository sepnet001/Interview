using System.Collections.Generic;

namespace Verivox.BLL.ServiceContracts
{
    public interface ITariffComparer
    {
        void Compare(double consumption);

        void Compare(IEnumerable<double> consumptions);
    }
}
