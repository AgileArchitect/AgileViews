using DocCoder.Model;

namespace DocCoder.Export
{
    public class PlantUmlContextStrategy : AbstractViewExporter
    {
        protected override string Header()
        {
            return @"
{% plantuml %}
skinparam componentStyle uml2  
skinparam rectangle {
  BorderColor black
  BackgroundColor white
}
skinparam component {
  FontSize 13
  InterfaceBackgroundColor white
  InterfaceBorderColor black
  FontName Helvetica
  BorderColor black
  BackgroundColor white
  ArrowFontName Helvetica
  ArrowColor #000000
  ArrowFontColor #000000
}          
sprite $user [16x16/16] {
0000019EE9100000
00000DFFFFD00000
00006FFFFFF60000
00009FFFFFF90000
00009FFFFFF90000
00006FFFFFF60000
00001FFFFFF10000
000008FFFF800000
000000DFFD000000
000002EFFE200000
0003BFFFFFFB3000
009FFFFFFFFFF900
08FFFFFFFFFFFF80
0CFFFFFFFFFFFFC0
07FFFFFFFFFFFF70
0038BDEFFEDB8300
}
";
        }

        protected override string Footer()
        {
            return "{% endplantuml %}";
        }

        protected override string ExportElement(Element element)
        {
            if (element is Person)
                return $"rectangle \"{element.Name}\" <<$user>> as {element.Alias}";
            if (element is Container)
                return $"[{element.Name}] as {element.Alias}";

            return "";
        }

        protected override string ExportRelationship(Relationship r)
        {
            return $"{r.Source.Alias} .> [{r.Target.Alias}]";
        }
    }

    public class PlantUmlContainerStrategy : AbstractViewExporter
    {
        protected override string Header()
        {
            return @"
{% plantuml %}
skinparam componentStyle uml2  
skinparam rectangle {
  BorderColor black
  BackgroundColor white
}
skinparam component {
  FontSize 13
  InterfaceBackgroundColor white
  InterfaceBorderColor black
  FontName Helvetica
  BorderColor black
  BackgroundColor white
  ArrowFontName Helvetica
  ArrowColor #000000
  ArrowFontColor #000000
}          
sprite $user [16x16/16] {
    0000019EE9100000
    00000DFFFFD00000
    00006FFFFFF60000
    00009FFFFFF90000
    00009FFFFFF90000
    00006FFFFFF60000
    00001FFFFFF10000
    000008FFFF800000
    000000DFFD000000
    000002EFFE200000
    0003BFFFFFFB3000
    009FFFFFFFFFF900
    08FFFFFFFFFFFF80
    0CFFFFFFFFFFFFC0
    07FFFFFFFFFFFF70
    0038BDEFFEDB8300
}
";
        }

        protected override string Footer()
        {
            return "{% endplantuml %}";
        }

        protected override string ExportElement(Element element)
        {
            if (element is Person)
                return $"rectangle \"{element.Name}\" <<$user>> as {element.Alias}";
            if (element is Container)
                return $"[{element.Name}] as {element.Alias}";

            return "";
        }

        protected override string ExportRelationship(Relationship r)
        {
            return $"{r.Source.Alias} .> [{r.Target.Alias}]";
        }
    }

}