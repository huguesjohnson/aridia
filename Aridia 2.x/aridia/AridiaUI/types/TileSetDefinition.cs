/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2015 Hugues Johnson

Aridia is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License version 2 
as published by the Free Software Foundation.

Aridia is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>. 
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