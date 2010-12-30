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
using System.Drawing;

namespace com.huguesjohnson.MegaDriveIO
{
	/// <summary>
	/// Class used to represent a 9-bit RGB palette entry.
	/// </summary>
	[Serializable]
	public class PaletteEntry
	{
		/// <summary>
		/// The Red value for this palette entry.
		/// </summary>
		private ushort r;
		/// <summary>
		/// The Green value for this palette entry.
		/// </summary>
		private ushort g;
		/// <summary>
		/// The Blue value for this palette entry.
		/// </summary>
		private ushort b;

		/// <summary>
		/// The Red value for this palette entry - must be 0-7, throws exception if value outside that range is passed.
		/// </summary>
		public ushort R
		{
			get
			{
				return(this.r);
			}
			set
			{
				if((value<0)||(value>7))
				{
					throw(new Exception("Palette entry color values must be [0-7], "+value+" is outside the valid range"));
				}
				else
				{
					this.r=value;
				}
			}
		}
		
		/// <summary>
		/// The Green value for this palette entry - must be 0-7, throws exception if value outside that range is passed.
		/// </summary>
		public ushort G
		{
			get
			{
				return(this.g);
			}
			set
			{
				if((value<0)||(value>7))
				{
					throw(new Exception("Palette entry color values must be [0-7], "+value+" is outside the valid range"));
				}
				else
				{
					this.g=value;
				}
			}
		}

		/// <summary>
		/// The Blue value for this palette entry - must be 0-7, throws exception if value outside that range is passed.
		/// </summary>
		public ushort B
		{
			get
			{
				return(this.b);
			}
			set
			{
				if((value<0)||(value>7))
				{
					throw(new Exception("Palette entry color values must be [0-7], "+value+" is outside the valid range"));
				}
				else
				{
					this.b=value;
				}
			}
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public PaletteEntry()
		{
            this.R=0;
			this.G=0;
			this.B=0;
		}

		/// <summary>
		/// Creates an instance of PaletteEntry based the value stored in the binary ROM image (unsigned 16-bit number)
		/// </summary>
		/// <param name="entry"></param>
		public PaletteEntry(ushort entry)
		{
			//convert to binary... 
			//there's probably a more efficient way to do this but it's a breeze to debug this way
			string binaryString=Convert.ToString(entry,2);
			int length=binaryString.Length;
			if(length<12)
			{
				binaryString=binaryString.PadLeft(12,'0');
			}
			else if(length>12)
			{
				binaryString=binaryString.Substring(length-12);
			}
			//parse values
			this.B=(ushort)((UInt16.Parse(binaryString.Substring(0,1))*4)+(UInt16.Parse(binaryString.Substring(1,1))*2)+(UInt16.Parse(binaryString.Substring(2,1))*1));
			this.G=(ushort)((UInt16.Parse(binaryString.Substring(4,1))*4)+(UInt16.Parse(binaryString.Substring(5,1))*2)+(UInt16.Parse(binaryString.Substring(6,1))*1));
			this.R=(ushort)((UInt16.Parse(binaryString.Substring(8,1))*4)+(UInt16.Parse(binaryString.Substring(9,1))*2)+(UInt16.Parse(binaryString.Substring(10,1))*1));
		}

		/// <summary>
		/// Returns a standard RGB representation of this palette entry.
		/// </summary>
		/// <returns>A standard RGB representation of this palette entry</returns>
		public Color ToRGB()
		{
			//shifted by 5 to go from 3 to 8 bits
			int rgbR=this.R<<5;
			int rgbG=this.G<<5;
			int rgbB=this.B<<5;
			return(Color.FromArgb(255,rgbR,rgbG,rgbB));
		}

		/// <summary>
		/// Returns an integer representation of this object.
		/// </summary>
		/// <returns>An integer representation of this object.</returns>
		public ushort ToUInt()
		{
			//there's probably a more efficient way to do this but it's a breeze to debug this way
			string binaryB=Convert.ToString(this.B,2);
			if(binaryB.Length<3){ binaryB=binaryB.PadLeft(3,'0'); }
			string binaryG=Convert.ToString(this.G,2);
			if(binaryG.Length<3){ binaryG=binaryG.PadLeft(3,'0'); }
			string binaryR=Convert.ToString(this.R,2);
			if(binaryR.Length<3){ binaryR=binaryR.PadLeft(3,'0'); }
			string binary=binaryB+"0"+binaryG+"0"+binaryR+"0";
			ushort toUInt=0;
			for(int index=0;index<12;index++)
			{
				int power=11-index;
				if(binary.Substring(index,1).Equals("1"))
				{
					toUInt+=(ushort)Math.Pow(2,power);
				}
			}
			return(toUInt);
		}
	}
}
