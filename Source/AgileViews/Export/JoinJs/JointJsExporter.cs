using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgileViews.Export.Jekyll;
using AgileViews.Model;

namespace AgileViews.Export.JoinJs
{
    class JointJsExporter : IJekyllViewExporter
    {
        public void Export(JekyllExporter exporter, View view, StreamWriter writer)
        {
            writer.WriteLine(@"<script src=""https://cdnjs.cloudflare.com/ajax/libs/jquery/2.0.3/jquery.min.js"" />");
            writer.WriteLine(@"<script src=""https://cdnjs.cloudflare.com/ajax/libs/lodash.js/3.10.1/lodash.min.js"" />");
            writer.WriteLine(@"<script src=""https://cdnjs.cloudflare.com/ajax/libs/backbone.js/1.2.1/backbone-min.js"" />");
            writer.WriteLine(@"<script src=""https://cdnjs.cloudflare.com/ajax/libs/jointjs/0.9.7/joint.min.js"" />");
            writer.WriteLine(@"<link rel=""stylesheet"" type=""text/css"" href=""https://cdnjs.cloudflare.com/ajax/libs/jointjs/0.9.7/joint.min.css"" />");

            writer.WriteLine(@"
<div id='paper'></div>
<script>
var graph = new joint.dia.Graph();

var paper = new joint.dia.Paper({
    el: $('#paper'),
    width: 800,
    height: 600,
    gridSize: 1,
    model: graph
});

function state(x, y, label) {
    
    var cell = new joint.shapes.fsa.State({
        position: { x: x, y: y },
        size: { width: 60, height: 60 },
        attrs: {
            text : { text: label, fill: '#000000', 'font-weight': 'normal' },
            'circle': {
                fill: '#f6f6f6',
                stroke: '#000000',
                'stroke-width': 2
            }
        }
    });
    graph.addCell(cell);
    return cell;
}

function link(source, target, label, vertices) {
    
    var cell = new joint.shapes.fsa.Arrow({
        source: { id: source.id },
        target: { id: target.id },
        labels: [{ position: 0.5, attrs: { text: { text: label || '', 'font-weight': 'bold' } } }],
        vertices: vertices || []
    });
    graph.addCell(cell);
    return cell;
}

var start = new joint.shapes.fsa.StartState({ position: { x: 50, y: 530 } });
graph.addCell(start);

var code  = state(180, 390, 'code');
var slash = state(340, 220, 'slash');
var star  = state(600, 400, 'star');
var line  = state(190, 100, 'line');
var block = state(560, 140, 'block');

link(start, code,  'start');
link(code,  slash, '/');
link(slash, code,  'other', [{x: 270, y: 300}]);
link(slash, line,  '/');
link(line,  code,  'new\n line');
link(slash, block, '*');
link(block, star,  '*');
link(star,  block, 'other', [{x: 650, y: 290}]);
link(star,  code,  '/',     [{x: 490, y: 310}]);
link(line,  line,  'other', [{x: 115,y: 100}, {x: 250, y: 50}]);
link(block, block, 'other', [{x: 485,y: 140}, {x: 620, y: 90}]);
link(code,  code,  'other', [{x: 180,y: 500}, {x: 305, y: 450}]);
</script>
");
        }
    }
}
