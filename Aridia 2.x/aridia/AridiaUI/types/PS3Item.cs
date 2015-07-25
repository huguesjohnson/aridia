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