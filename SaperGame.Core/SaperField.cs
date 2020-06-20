using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SniperGame.Core
{
    public class SaperField
    {
        public int Value { get; set; }

        public bool Visible { get; set; }

        public SaperField(int value, bool visible)
        {
            Value = value;
            Visible = visible;
        }
    }
}
