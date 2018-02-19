/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2018 Hugues Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections;

namespace com.huguesjohnson.aridia.MegaDriveIO
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
