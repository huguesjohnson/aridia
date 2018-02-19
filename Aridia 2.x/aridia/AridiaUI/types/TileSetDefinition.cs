/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2018 Hugues Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.huguesjohnson.aridia.types
{
    [Serializable]
    public class TileSetDefinition
    {
        private string _name;
        public string name
        {
            get{return(_name);}
            set{_name=value;}
        }

        private int _startAddress;
        public int startAddress
        {
            get{return(_startAddress);}
            set{_startAddress=value;}
        }

        private int _endAddress;
        public int endAddress
        {
            get{return(_endAddress);}
            set{_endAddress=value;}
        }
        
        private int _columns;
        public int columns
        {
            get{return(_columns);}
            set{_columns=value;}
        }

        private int _rows;
        public int rows
        {
            get{return(_rows);}
            set{_rows=value;}
        }

        private int _tileWidth;
        public int tileWidth
        {
            get{return(_tileWidth);}
            set{_tileWidth=value;}
        }

        private int _tileHeight;
        public int tileHeight
        {
            get{return(_tileHeight);}
            set{_tileHeight=value;}
        }

        public override string ToString()
        {
            return(_name);
        }

    }
}
