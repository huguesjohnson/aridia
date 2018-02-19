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
