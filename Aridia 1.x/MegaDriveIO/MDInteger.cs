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
	/// Byte order.
	/// HighByteFirst means [01 00] = 256
	/// LowByteFirst means [01 00] = 1
	/// </summary>
	public enum ByteOrder : int
	{
		HighByteFirst=0,
		LowByteFirst=1
	}

	/// <summary>
	/// Representation of an integer value in a MegaDrive ROM image.
	/// </summary>
	[Serializable]
	public class MDInteger : BaseMDData
	{
		/// <summary>
		/// Default value if none is specified.
		/// public const int DEFAULT_BYTE_ORDER=ByteOrder.HighByteFirst;
		/// </summary>
		public const int DEFAULT_BYTE_ORDER=(int)ByteOrder.HighByteFirst;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public MDInteger()
		{ 
			this.lookupTableName=null;
			this.byteOrder=(ByteOrder)DEFAULT_BYTE_ORDER;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="address">The address (decimal) in the MegaDrive ROM where this data begins.</param>
		/// <param name="numBytes">The number of bytes (or length) for this data field.</param>
		/// <param name="description">A description of what this data represents, i.e. "Hit points for main character".</param>
		/// <param name="currentValue">The current integer value.</param>
		/// <param name="lookupTableName">Name of LookupTable to reference for valid values, null indicates no corresponding LookupTable.</param>
		/// <param name="maxValue">Maximum value that can be assigned to this integer.</param>
		/// <param name="minValue">Minimim value that can be assigned to this integer.</param>
		public MDInteger(int address,int numBytes,string description,int currentValue,string lookupTableName,int maxValue,int minValue) : base(address,numBytes,description)
		{
			this.byteOrder=(ByteOrder)DEFAULT_BYTE_ORDER;
			this.currentValue=currentValue;
			this.lookupTableName=lookupTableName;
			this.maxValue=maxValue;
			this.minValue=minValue;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="address">The address (decimal) in the MegaDrive ROM where this data begins.</param>
		/// <param name="numBytes">The number of bytes (or length) for this data field.</param>
		/// <param name="description">A description of what this data represents, i.e. "Hit points for main character".</param>
		/// <param name="maxValue">Maximum value that can be assigned to this integer.</param>
		/// <param name="minValue">Minimim value that can be assigned to this integer.</param>
		public MDInteger(int address,int numBytes,string description,int maxValue,int minValue) : base(address,numBytes,description)
		{
			this.byteOrder=(ByteOrder)DEFAULT_BYTE_ORDER;
			this.lookupTableName=null;
			this.maxValue=maxValue;
			this.minValue=minValue;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="address">The address (decimal) in the MegaDrive ROM where this data begins.</param>
		/// <param name="numBytes">The number of bytes (or length) for this data field.</param>
		/// <param name="description">A description of what this data represents, i.e. "Hit points for main character".</param>
		/// <param name="maxValue">Maximum value that can be assigned to this integer.</param>
		/// <param name="minValue">Minimim value that can be assigned to this integer.</param>
		/// <param name="byteOrder">Byte order for this integer.</param>
		public MDInteger(int address,int numBytes,string description,int maxValue,int minValue,ByteOrder byteOrder) : base(address,numBytes,description)
		{
			this.lookupTableName=null;
			this.maxValue=maxValue;
			this.minValue=minValue;
			this.byteOrder=byteOrder;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="address">The address (decimal) in the MegaDrive ROM where this data begins.</param>
		/// <param name="numBytes">The number of bytes (or length) for this data field.</param>
		/// <param name="description">A description of what this data represents, i.e. "Hit points for main character".</param>
		/// <param name="lookupTableName">Name of LookupTable to reference for valid values, null indicates no corresponding LookupTable.</param>
		/// <param name="maxValue">Maximum value that can be assigned to this integer.</param>
		/// <param name="minValue">Minimim value that can be assigned to this integer.</param>
		/// <param name="byteOrder">Byte order for this integer.</param>
		public MDInteger(int address,int numBytes,string description,string lookupTableName,int maxValue,int minValue,ByteOrder byteOrder) : base(address,numBytes,description)
		{
			this.lookupTableName=lookupTableName;
			this.maxValue=maxValue;
			this.minValue=minValue;
			this.byteOrder=byteOrder;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="address">The address (decimal) in the MegaDrive ROM where this data begins.</param>
		/// <param name="numBytes">The number of bytes (or length) for this data field.</param>
		/// <param name="description">A description of what this data represents, i.e. "Hit points for main character".</param>
		/// <param name="lookupTableName">Name of LookupTable to reference for valid values, null indicates no corresponding LookupTable.</param>
		/// <param name="maxValue">Maximum value that can be assigned to this integer.</param>
		/// <param name="minValue">Minimim value that can be assigned to this integer.</param>
		public MDInteger(int address,int numBytes,string description,string lookupTableName,int maxValue,int minValue) : base(address,numBytes,description)
		{
			this.byteOrder=(ByteOrder)DEFAULT_BYTE_ORDER;
			this.lookupTableName=lookupTableName;
			this.maxValue=maxValue;
			this.minValue=minValue;
		}

		/// <summary>
		/// The current integer value.
		/// </summary>
		[NonSerialized]
		private int currentValue;

		/// <summary>
		/// Name of LookupTable to reference for valid values, null indicates no corresponding LookupTable.
		/// </summary>
		private string lookupTableName;

		/// <summary>
		/// Maximum value that can be assigned to this integer.
		/// </summary>
		private int maxValue;

		/// <summary>
		/// Minimum value that can be assigned to this integer.
		/// </summary>
		private int minValue;

		/// <summary>
		/// Byte order for this integer.
		/// </summary>
		private ByteOrder byteOrder;

		/// <summary>
		/// The current integer value.
		/// </summary>
		public int CurrentValue
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

		/// <summary>
		/// Name of LookupTable to reference for valid values, null indicates no corresponding LookupTable.
		/// </summary>
		public string LookupTableName
		{
			get
			{
				return(this.lookupTableName);
			}
			set
			{
				this.lookupTableName=value;
			}
		}

		/// <summary>
		/// Maximum value that can be assigned to this integer.
		/// </summary>
		public int MaxValue
		{
			get
			{
				return(this.maxValue);
			}
			set
			{
				this.maxValue=value;
			}
		}

		/// <summary>
		/// Minimum value that can be assigned to this integer.
		/// </summary>
		public int MinValue
		{
			get
			{
				return(this.minValue);
			}
			set
			{
				this.minValue=value;
			}
		}

		/// <summary>
		/// Byte order for this integer.
		/// </summary>
		public ByteOrder ByteOrder
		{
			get
			{
				return(this.byteOrder);
			}
			set
			{
				this.byteOrder=value;
			}
		}
	}
}
