/*
MegaDriveIO: Utilities to read/write a Mega Drive binary ROM image
Originally created for Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2010 Hugues Johnson

MegaDriveIO is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License version 2 
as published by the Free Software Foundation.

MegaDriveIO is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;

namespace com.huguesjohnson.MegaDriveIO
{
	/// <summary>
	/// Contains fields common to all MegaDrive data types.
	/// </summary>
	[Serializable]
	public abstract class BaseMDData
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public BaseMDData(){ }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="address">The address (decimal) in the MegaDrive ROM where this data begins.</param>
		/// <param name="numBytes">The number of bytes (or length) for this data field.</param>
		/// <param name="description">A description of what this data represents, i.e. "Hit points for main character".</param>
		public BaseMDData(int address,int numBytes,string description)
		{
			this.address=address;
			this.numBytes=numBytes;
			this.description=description;
		}

		/// <summary>
		/// The address (decimal) in the MegaDrive ROM where this data begins.
		/// </summary>
		private int address;

		/// <summary>
		/// The number of bytes (or length) for this data field.
		/// </summary>
		private int numBytes;

		/// <summary>
		/// A description of what this data represents, i.e. "Hit points for main character".
		/// </summary>
		private string description;

		/// <summary>
		/// The address (decimal) in the MegaDrive ROM where this data begins.
		/// </summary>
		public int Address
		{
			get
			{
				return(this.address);
			}
			set
			{
				this.address=value;
			}
		}

		/// <summary>
		/// The number of bytes (or length) for this data field.
		/// </summary>
		public int NumBytes
		{
			get
			{
				return(this.numBytes);
			}
			set
			{
				this.numBytes=value;
			}
		}

		/// <summary>
		/// A description of what this data represents, i.e. "Hit points for main character".
		/// </summary>
		public string Description
		{
			get
			{
				return(this.description);
			}
			set
			{
				this.description=value;
			}
		}
	
	}
}
