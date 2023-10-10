using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Model
{
    public partial class GrassModel : CellModel 
    {
        public GrassModel() : base("Grass", CellKind.WASTELAND) { }
    }
}