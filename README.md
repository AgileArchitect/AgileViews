# AgileViews

![Master Status](https://ci.appveyor.com/api/projects/status/github/AgileArchitect/AgileViews?svg=true)

Generate architecture documentation from definition and code, and compare!

* Keep your architecture documentation up-to-date
* Visualise your goal architecture
* Highlight work to be done
* Highlight technical debth

Some goals:
* Generate durable architecture documentation
* Use design information in code to generate documentation
* Allow ist (defined) and sol (generated) models and show delta

Output
* Jekyll pages
* Plain markdown
* Inline SVG (layouted using MSAGL)
* ... ?


# Sample

```c#
    public class Program
    {
        static void Main(string[] args)
        {
            var workspace = new Workspace("Hi");
            var model = workspace.GetModel();

            new ReflectionProcessor().Default().Process(typeof(Program).Assembly, model);

            workspace.CreateView(model.ElementByName(typeof (Program).FullName), ViewType.Classes);

            workspace.Export(".");
        }
    }
```

