using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileViews.Model
{
    public interface IProcessor
    {
        void Process(Model model);
    }
}
