/*
IPSCreator: Utility to create an IPS file
Originally created for Aridia: Phantasy Star III ROM Editor
Copyright (c) 2011 Hugues Johnson

TileEditor is free software; you can redistribute it and/or modify
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

namespace com.huguesjohnson.IPSCreator
{
    class PatchRecord
    {
        /// <summary>
        /// Create a new instance of PatchRecord.
        /// </summary>
        /// <param name="address">The starting address for the patch record.</param>
        public PatchRecord(int address) 
        {
            this.Address=address;
        }

        /// <summary>
        /// The starting address for the patch record.
        /// </summary>
        private int address;

        /// <summary>
        /// The data for the patch record.
        /// </summary>
        private byte[] data;

        /// <summary>
        /// The starting address for the patch record.
        /// </summary>
        public int Address
        {
            get{return(this.address);}
            set{this.address=value;}
        }

        /// <summary>
        /// The data for the patch record.
        /// </summary>
        public byte[] Data
        {
            get{return(this.data);}
            set{this.data=value;}
        }

        /// <summary>
        /// The length of the data for the patch record.
        /// </summary>
        public int Length 
        {
            get{
                if(this.data==null)
                {
                    return(0);
                }
                else
                {
                    return(this.data.Length);
                }
            }
        }
    }
}
