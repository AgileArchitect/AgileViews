using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocCoder.Model
{
    public class Workspace
    {
        private Model _model = new Model();
        public Model GetModel()
        {
            return _model;
        }

        public View CreateContextView(System element)
        {
            var view = new View(element, new AllRelationshipStrategy());
            view.ViewType = ViewType.Context;
            view.Model = GetModel();
            return view;
        }

        public View CreateContainerView(System element)
        {
            var view = new View(element, new AllRelationshipStrategy());
            view.ViewType = ViewType.Containers;
            view.Model = GetModel();
            return view;
        }
    }
}
