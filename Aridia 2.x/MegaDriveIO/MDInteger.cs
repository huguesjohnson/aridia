/*
MegaDriveIO: Utilities to read/write a Mega Drive binary ROM image
Originally created for Aridia: Phantasy Star III ROM Editor
Modified for Eisfrei: Herzog Zwei ROM Editor
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
