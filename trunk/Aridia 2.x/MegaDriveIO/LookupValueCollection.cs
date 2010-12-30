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
using System.Collections;

namespace com.huguesjohnson.MegaDriveIO
{
	/// <summary>
	/// Stores a collection of LookupValue objects.
	/// </summary>
	[Serializable]
	public class LookupValueCollection
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public LookupValueCollection()
		{ 
			this.collection=new ArrayList();
		}

		/// <summary>
		/// The underlying collection that stores all the items.
		/// </summary>
		private ArrayList collection;
		/// <summary>
		/// The underlying collection that stores all the items.
		/// </summary>
		public ArrayList Collection
		{
			get{ return(this.collection);}
			set{ this.collection=value; }
		}

		/// <summary>
		/// Adds and item to the collection.
		/// </summary>
		/// <param name="lookupValue"></param>
		public void add(LookupValue lookupValue)
		{
			this.collection.Add(lookupValue);
		}

		/// <summary>
		/// Returns the number of items in the collection.
		/// </summary>
		/// <returns>The number of items in the collection.</returns>
		public int getSize()
		{
			return(this.collection.Count);
		}

		/// <summary>
		/// Returns the first item in the collection with the given description.
		/// </summary>
		/// <param name="description">The description to search for.</param>
		/// <returns>The first item in the collection with the description, otherwise null.</returns>
		public LookupValue getByDescription(string description)
		{
			int size=this.getSize();
			for(int index=0;index<size;index++)
			{
				LookupValue testValue=(LookupValue)this.collection[index];
				if(testValue.Description.Equals(description))
				{
					return(testValue);
				}
			}
			return(null);
		}

		/// <summary>
		/// Returns the first item in the collection with the given value.
		/// </summary>
		/// <param name="description">The value to search for.</param>
		/// <returns>The first item in the collection with the value, otherwise null.</returns>
		public LookupValue getByValue(int intValue)
		{
			int size=this.getSize();
			for(int index=0;index<size;index++)
			{
				LookupValue testValue=(LookupValue)this.collection[index];
				if(testValue.IntValue==intValue)
				{
					return(testValue);
				}
			}
			return(null);
		}

		/// <summary>
		/// Returns all the items in the collection.
		/// </summary>
		/// <returns>All the items in the collection.</returns>
		public LookupValue[] getAll()
		{
			return((LookupValue[])this.collection.ToArray(typeof(LookupValue)));
		}
	}
}
