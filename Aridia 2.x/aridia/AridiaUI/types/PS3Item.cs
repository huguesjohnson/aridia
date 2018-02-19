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

using com.huguesjohnson.MegaDriveIO;

namespace com.huguesjohnson.aridia.types
{
	/// <summary>
	/// Extremely simple class to transport items.
	/// </summary>
	public class PS3Item
	{
		private string _hexString;
        public string hexString
        {
            get{return(_hexString);}
            set{_hexString=value;}
        }
		
        private bool _isEquipped;
        public bool isEquipped
        {
            get{return(_isEquipped);}
            set{_isEquipped=value;}
        }

        private LookupValue _itemLookup;
        public LookupValue itemLookup 
        { 
            get{return(_itemLookup);}
            set{_itemLookup=value;}
        }

        private LookupValue _whereEquipped;
        public LookupValue whereEquipped
        { 
            get{return(_whereEquipped);}
            set{_whereEquipped=value;}
        }

	}
}
