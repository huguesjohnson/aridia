/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2015 Hugues Johnson

Aridia is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License version 2 
as published by the Free Software Foundation.

Aridia is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace com.huguesjohnson.aridia.types
{
    [Serializable]
    [XmlInclude(typeof(TileSetDefinition))]
    public class TileSetDefinitionCollection
    {
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TileSetDefinitionCollection()
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
			get{return(this.collection);}
			set{this.collection=value; }
		}

		/// <summary>
		/// Adds and item to the collection.
		/// </summary>
		public void add(TileSetDefinition tsd)
		{
			this.collection.Add(tsd);
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
		public TileSetDefinition getByName(string name)
		{
			int size=this.getSize();
			for(int index=0;index<size;index++)
			{
				TileSetDefinition testValue=(TileSetDefinition)this.collection[index];
				if(testValue.name.Equals(name))
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
		public TileSetDefinition[] getAll()
		{
			return((TileSetDefinition[])this.collection.ToArray(typeof(TileSetDefinition)));
		}
    }
}
