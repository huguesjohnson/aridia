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
	/// Class used to represent an uncompressed tile.
	/// </summary>
	public class MDTile
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public MDTile(){ }

		/// <summary>
		/// The address (decimal) in the MegaDrive ROM where this data begins.
		/// </summary>
		private int address;	

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
		/// The pixel data for the tile.
		/// </summary>
		private byte[][] pixelData;

		/// <summary>
		/// The pixel data for the tile.
		/// </summary>
		public byte[][] PixelData
		{
			get
			{
				return(this.pixelData);
			}
			set
			{
				this.pixelData=value;
			}
		}
	}
}
