﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWCompil.State
{
    public class ErrorData
    {
        public int Line { get; set; }
        public int Index { get; set; }
        public string Text { get; set; }
        public ErrorData(int line, int index, string text) 
        {
            Line = line;
            Index = index;
            Text = text;
        }

    }
}
