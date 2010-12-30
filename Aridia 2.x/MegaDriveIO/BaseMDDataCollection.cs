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
	/// Stores a collection of BaseMDData objects.
	/// </summary>
	[Serializable]
	public class BaseMDDataCollection
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public BaseMDDataCollection()
		{ 
			this.collection=new ArrayList();
		}

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
		/// <param name="mdData"></param>
		public void add(BaseMDData mdData)
		{
			this.collection.Add(mdData);
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
		/// Returns all the items in the collection.
		/// </summary>
		/// <returns>All the items in the collection.</returns>
		public BaseMDData[] getAll()
		{
			return((BaseMDData[])this.collection.ToArray(typeof(BaseMDData)));
		}
	}
}
