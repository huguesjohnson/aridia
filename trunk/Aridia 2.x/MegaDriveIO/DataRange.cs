/*
MegaDriveIO: Utilities to read/write a Mega Drive binary ROM image
Originally created for Eisfrei: Herzog Zwei ROM Editor
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
	/// Used to represent where a range of data starts and ends.
	/// </summary>
	[Serializable]
	public class DataRange
	{
		/// <summary>
		/// Default constuctor.
		/// </summary>
		public DataRange(){ }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="description">The description.</param>
		/// <param name="startAddress">The start address for the data range.</param>
		/// <param name="endAddress">The end address for the data range.</param>
		public DataRange(string description,int startAddress,int endAddress)
		{
			this.Description=description;
			this.StartAddress=startAddress;
			this.EndAddress=endAddress;
		}

		/// <summary>
		/// The start address for the data range.
		/// </summary>
		private int startAddress;

		/// <summary>
		/// The end address for the data range.
		/// </summary>
		private int endAddress;

		/// <summary>
		/// The description.
		/// </summary>
		private string description;

		/// <summary>
		/// The start address for the data range.
		/// </summary>
		public int StartAddress
		{
			get
			{
				return(this.startAddress);
			}
			set
			{
				this.startAddress=value;
			}
		}

		/// <summary>
		/// The end address for the data range.
		/// </summary>
		public int EndAddress
		{
			get
			{
				return(this.endAddress);
			}
			set
			{
				this.endAddress=value;
			}
		}

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
	}
}
