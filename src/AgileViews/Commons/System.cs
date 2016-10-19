using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgileViews.Model;

namespace AgileViews.Commons
{
    public class SoftwareSystem
    {
        public static string Kind = typeof(SoftwareSystem).Kind();
    }

    public class Component
    {
        public static string Kind = typeof(Component).Kind();
    }

    public class Entity
    {
        public static string Kind = typeof(Entity).Kind();
    }

    public class Message
    {
        public static string Kind = typeof(Message).Kind();
    }

    public class Event : Message
    {
        public new static string Kind = typeof(Event).Kind();
    }

    public class Command : Message
    {
        public new static string Kind = typeof(Command).Kind();
    }
}
