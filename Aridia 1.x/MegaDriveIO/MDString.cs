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
	/// Representation of a string value in a MegaDrive ROM image.
	/// </summary>
	public class MDString : com.huguesjohnson.aridia.MegaDriveIO.BaseMDData
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
