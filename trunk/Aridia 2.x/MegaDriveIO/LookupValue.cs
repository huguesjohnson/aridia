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
	/// Maps an integer value to a string description.
	/// </summary>
	[Serializable]
	public class LookupValue
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public LookupValue(){ }

		/// <summary>
		/// Constructor.
		/// </summary>
		public LookupValue(string description,int intValue)
		{
			this.Description=description;
			this.IntValue=intValue;
		}

		/// <summary>
		/// The description.
		/// </summary>
		private string description;

		/// <summary>
		/// The value.
		/// </summary>
		private int intValue;

		/// <summary>
		/// The description.
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

		/// <summary>
		/// The value.
		/// </summary>
		public int IntValue
		{
			get
			{
				return(this.intValue);
			}		
			set
			{
				this.intValue=value;
			}
		}

		/// <summary>
		/// Returns the description.
		/// </summary>
		/// <returns>Description (used for display in a combobox).</returns>
		public override string ToString()
		{
			return(this.description);
		}
	}
}
