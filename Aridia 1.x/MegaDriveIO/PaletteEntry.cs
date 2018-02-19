/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2018 Hugues Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Drawing;

namespace com.huguesjohnson.aridia.MegaDriveIO
{
	/// <summary>
	/// Class used to represent a 9-bit RGB palette entry.
	/// </summary>
	[Serializable]
	public class PaletteEntry
	{
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
