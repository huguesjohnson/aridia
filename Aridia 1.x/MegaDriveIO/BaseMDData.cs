/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2018 Hugues Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;

namespace com.huguesjohnson.aridia.MegaDriveIO
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
