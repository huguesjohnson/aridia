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
	/// Representation of a string value in a MegaDrive ROM image.
	/// </summary>
	public class MDString : com.huguesjohnson.MegaDriveIO.BaseMDData
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public MDString()
		{ 
			this.currentValue=null;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="address">The address (decimal) in the MegaDrive ROM where this data begins.</param>
		/// <param name="numBytes">The number of bytes (or length) for this data field.</param>
		/// <param name="description">A description of what this data represents, i.e. "Hit points for main character".</param>
		/// <param name="currentValue">The current integer value.</param>
		public MDString(int address,int numBytes,string description,string currentValue) : base(address,numBytes,description)
		{
			this.currentValue=currentValue;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="address">The address (decimal) in the MegaDrive ROM where this data begins.</param>
		/// <param name="numBytes">The number of bytes (or length) for this data field.</param>
		/// <param name="description">A description of what this data represents, i.e. "Hit points for main character".</param>
		public MDString(int address,int numBytes,string description) : base(address,numBytes,description)
		{
			this.currentValue=null;
		}

		/// <summary>
		/// The current string value.
		/// </summary>
		[NonSerialized]
		private string currentValue;

		/// <summary>
		/// The current string value.
		/// </summary>
		public string CurrentValue
		{
			get
			{
				return(this.currentValue);
			}
			set
			{
				this.currentValue=value;
			}
		}

	}
}
