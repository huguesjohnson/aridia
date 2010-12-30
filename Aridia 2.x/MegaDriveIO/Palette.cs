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
	/// Class used to represent a 16-color palette.
	/// </summary>
	[Serializable]
	public class Palette
	{
		/// <summary>
		/// The number of entries in the palette.
		/// </summary>
		public const int SIZE=16;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Palette()
		{
			this.Entries=new PaletteEntry[SIZE];
		}
		
		/// <summary>
		/// The individual palette entries.
		/// </summary>
		private PaletteEntry[] entries;

		/// <summary>
		/// The individual palette entries.
		/// </summary>
		public PaletteEntry[] Entries
		{
			get
			{
				return(this.entries);
			}
			set
			{
				if(value.Length!=SIZE)
				{
					throw(new Exception("Array length must be equal to Palette.SIZE ["+SIZE+"], size passed is ["+value.Length+"]."));
				}
				else
				{
					this.entries=value;
				}
			}
		}
	}
}
