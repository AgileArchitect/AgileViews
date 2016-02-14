using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocCoder.Model
{
    public class Message : Element
    {
        public override Element GetParent()
        {
            return null;
        }

        public MessageType Type { get; set; }
        public ExecutionType Execution { get; set; }

    }
}
