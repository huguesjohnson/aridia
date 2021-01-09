/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2021 Hugues Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

using com.huguesjohnson.aridia.types;
using com.huguesjohnson.MegaDriveIO;

namespace com.huguesjohnson.aridia.ui
{
	/// <summary>
	/// Contains static utility methods used by AridiaUI.
	/// </summary>
	public abstract class AridiaUtils
	{
        public const byte STRING_TERMINATOR=(byte)252;
        private static string dataPath;
		private static LookupValueCollection equipCodes;
		private static LookupValueCollection itemCodes;
		private static LookupValueCollection statGrowthCodes;
		private static LookupValueCollection buttonMaskCodes;
        private static TileSetDefinitionCollection tileSetDefinitions;

		/// <summary>
		/// The full path to where data files are stored.
		/// </summary>
		public static string DataPath
		{
			get
			{
				if(dataPath==null)
				{
					dataPath=Application.ExecutablePath;
					int indexOf=dataPath.LastIndexOf(Path.DirectorySeparatorChar)+1;
					dataPath=dataPath.Substring(0,indexOf)+@"data"+Path.DirectorySeparatorChar;
				}
				return(dataPath);
			}
		}

		/// <summary>
		/// Collection of equip codes.
		/// </summary>
		public static LookupValueCollection EquipCodes
		{
			get
			{
				if(equipCodes==null)
				{
					equipCodes=(LookupValueCollection)deserialize(getLookupValueCollectionSerializer(),"Equip-Codes");
				}
				return(equipCodes);
			}
		}

		/// <summary>
		/// Collection of item codes.
		/// </summary>
		public static LookupValueCollection ItemCodes
		{
			get
			{
				if(itemCodes==null)
				{
					itemCodes=(LookupValueCollection)deserialize(getLookupValueCollectionSerializer(),"Item-Codes");
				}
				return(itemCodes);
			}
		}

		/// <summary>
		/// Collection of statistic growth codes.
		/// </summary>
		public static LookupValueCollection StatGrowthCodes
		{
			get
			{
				if(statGrowthCodes==null)
				{
					statGrowthCodes=(LookupValueCollection)deserialize(getLookupValueCollectionSerializer(),"Stat-Growth");
				}
				return(statGrowthCodes);
			}
		}

		/// <summary>
		/// Collection of button mask codes.
		/// </summary>
		public static LookupValueCollection ButtonMaskCodes
		{
			get
			{
				if(buttonMaskCodes==null)
				{
					buttonMaskCodes=(LookupValueCollection)deserialize(getLookupValueCollectionSerializer(),"ButtonMask-Codes");
				}
				return(buttonMaskCodes);
			}
		}

        /// <summary>
		/// List of tile sets that can be edited.
		/// </summary>
		public static TileSetDefinitionCollection TileSetDefinitions
		{
			get
			{
				if(tileSetDefinitions==null)
				{
					tileSetDefinitions=(TileSetDefinitionCollection)deserialize(getTileSetDefinitionCollectionSerializer(),"TileSets");
				}
				return(tileSetDefinitions);
			}
		}

        /// <summary>
		/// Loads a level table.
		/// </summary>
		/// <param name="address">The address to start reading from.</param>
		/// <param name="romIO">The IMegaDriveIO to load values from.</param>
		public static ListViewItem[] getLevelTable(int address,IMegaDriveIO romIO)
		{
			ArrayList items=new ArrayList();
			int entryCount=romIO.readInteger(address,1);
			int currentOffset=address+1;
			for(int entryIndex=0;entryIndex<entryCount;entryIndex++)
			{
				int xp=romIO.readInteger(currentOffset,4);
				int level=entryIndex+2;
				items.Add(new ListViewItem(new string[]{xp.ToString(),level.ToString(),currentOffset.ToString()}));
				currentOffset+=4;
			}
			return((ListViewItem[])items.ToArray(typeof(ListViewItem)));
		}

		/// <summary>
		/// Loads list items for the game script.
		/// </summary>
		/// <param name="startAddress">The address to start reading from.</param>
		/// <param name="endAddress">The address to stop reading at.</param>
		/// <param name="romIO">The IMegaDriveIO to load values from.</param>
		public static ListViewItem[] getScriptItems(int startAddress,int endAddress,IMegaDriveIO romIO)
		{
			ArrayList items=new ArrayList();
			int currentOffset=startAddress-1;
			while(currentOffset<endAddress)
			{
				//find the start of the next string - there are junk characters between the strings
                string nextChar=romIO.readString(currentOffset,1,AridiaUtils.STRING_TERMINATOR);
				while(!(new Regex(@"^[a-zA-Z0-9]")).IsMatch(nextChar))
				{
					currentOffset++;
                    nextChar=romIO.readString(currentOffset,1,AridiaUtils.STRING_TERMINATOR);
				}
				//found the next starting position, read the string
                string scriptEntry=romIO.readString(currentOffset,65535,AridiaUtils.STRING_TERMINATOR);
				int length=scriptEntry.Length;
				int address=currentOffset;
				items.Add(new ListViewItem(new string[]{scriptEntry,address.ToString(),length.ToString()}));
				currentOffset+=length+2;
			}
			return((ListViewItem[])items.ToArray(typeof(ListViewItem)));
		}

		/// <summary>
		/// Load list items for the dialog list view.
		/// </summary>
		/// <param name="categoryName">The category to load (i.e. "Technques")</param>
		/// <param name="romIO">The IMegaDriveIO to load values from.</param>
		/// <returns>ListViewItem[]</returns>
		public static ListViewItem[] getDialogListItems(string categoryName,IMegaDriveIO romIO)
		{
			BaseMDDataCollection collection=(BaseMDDataCollection)deserialize(getBaseMDDataCollectionSerializer(),"Dialog-"+categoryName);
			BaseMDData[] allItems=collection.getAll();
			int size=allItems.Length;
			ListViewItem[] items=new ListViewItem[size];
			for(int index=0;index<size;index++)
			{
				int address=allItems[index].Address;
				int length=allItems[index].NumBytes;
                string currentValue=romIO.readString(address,length,AridiaUtils.STRING_TERMINATOR); 
				string description=allItems[index].Description;
				items[index]=new ListViewItem(new string[]{currentValue,categoryName,description,address.ToString(),length.ToString()});
			}
			return(items);
		}

		/// <summary>
		/// Load a LookupValueCollection into a combobox.
		/// </summary>
		/// <param name="comboBox">The combobox to populate.</param>
		/// <param name="listName">The name of the collection to load.</param>
		public static void loadLookupValues(System.Windows.Forms.ComboBox comboBox,string listName)
		{
			LookupValueCollection collection=getLookupValueCollection(listName);
			LookupValue[] allItems=collection.getAll();
			int size=allItems.Length;
			for(int index=0;index<size;index++)
			{
				comboBox.Items.Add(allItems[index]);
			}
		}

		/// <summary>
		/// Load a statistic growth values into a combobox.
		/// </summary>
		/// <param name="comboBox">The combobox to populate.</param>
		public static void loadStatGrowthValues(System.Windows.Forms.ComboBox comboBox)
		{
			comboBox.Items.Clear();
			LookupValue[] codes=StatGrowthCodes.getAll();
			int count=codes.Length;
			for(int index=0;index<count;index++)
			{
				comboBox.Items.Add(codes[index]);
			}
		}

		/// <summary>
		/// Load a LookupValueCollection into a listview.
		/// </summary>
		/// <param name="listView">The listview to populate.</param>
		/// <param name="listName">The name of the collection to load.</param>
		public static void loadLookupValues(System.Windows.Forms.ListView listView,string listName)
		{
			LookupValueCollection collection=getLookupValueCollection(listName);
			LookupValue[] allItems=collection.getAll();
			int size=allItems.Length;
			for(int index=0;index<size;index++)
			{
				listView.Items.Add(new ListViewItem(new string[]{allItems[index].Description,allItems[index].IntValue.ToString()}));
			}
		}

		/// <summary>
		/// Get a LookupValueCollection.
		/// </summary>
		/// <param name="listName">The name of the collection to load.</param>
		/// <returns>The requested LookupValueCollection.</returns>
		public static LookupValueCollection getLookupValueCollection(string listName)
		{
			return((LookupValueCollection)deserialize(getLookupValueCollectionSerializer(),listName));
		}

		/// <summary>
		/// Tests whether a string is valid (less than max length and only alphanumeric characters).
		/// </summary>
		/// <param name="mdString">The string to test.</param>
		/// <returns>True if valid.</returns>
		public static bool validateMDString(MDString mdString)
		{
            return(mdString.CurrentValue.Length<=mdString.NumBytes);
            /*
            if(mdString.CurrentValue.Length<=mdString.NumBytes)
            {
                bool isMatch=(new Regex(@"^[a-zA-Z0-9\s\.\,\!\?\'\xF8\xE4\x00]+$")).IsMatch(mdString.CurrentValue);
                return(isMatch);
            }
            return(false);
             */
        }

		/// <summary>
		/// Tests whether an MDInteger is valid (greater than zero and less than NumBytes*255+1)
		/// </summary>
		/// <param name="mdInt">The MDInteger to test.</param>
		/// <returns>True if valid.</returns>
		public static bool validateMDInteger(MDInteger mdInt)
		{
			double currentValue=mdInt.CurrentValue;
			double maxValue=Math.Pow(2.0,(double)(8*mdInt.NumBytes));
			return((currentValue>-1.0)&&(currentValue<maxValue));
		}

		/// <summary>
		/// Deserialize an XML file.
		/// </summary>
		/// <param name="serializer">The XmlSerializer to use.</param>
		/// <param name="fileName">The file to read from.</param>
		/// <returns></returns>
		private static object deserialize(XmlSerializer serializer,string fileName)
		{
			FileStream stream=null;
			try
			{
				stream=new FileStream(DataPath+fileName+".xdata",FileMode.Open,FileAccess.Read);
				return(serializer.Deserialize(stream));
			}
			finally
			{
				if(stream!=null){ stream.Close(); }
			}
		}

		/// <summary>
		/// Creates an XmlSerializer for collections of BaseMDData.
		/// </summary>
		/// <returns>An XmlSerializer for collections of BaseMDData.</returns>
		private static XmlSerializer getBaseMDDataCollectionSerializer()
		{
			return(new XmlSerializer(typeof(BaseMDDataCollection),new Type[]{(typeof(BaseMDData)),(typeof(MDString)),(typeof(MDInteger))}));
		}
		
		/// <summary>
		/// Creates an XmlSerializer for collections of LookupValue.
		/// </summary>
		/// <returns>An XmlSerializer for collections of LookupValue.</returns>
		private static XmlSerializer getLookupValueCollectionSerializer()
		{
			return(new XmlSerializer(typeof(LookupValueCollection),new Type[]{(typeof(LookupValue))}));
		}

		/// <summary>
		/// Creates an XmlSerializer for collections of TileSetDefinition.
		/// </summary>
		/// <returns>An XmlSerializer for collections of TileSetDefinition.</returns>
		private static XmlSerializer getTileSetDefinitionCollectionSerializer()
		{
			return(new XmlSerializer(typeof(TileSetDefinitionCollection),new Type[]{(typeof(TileSetDefinition))}));
		}

		/// <summary>
		/// Finds a lookup value in a combobox and sets it as the selected item.
		/// </summary>
		/// <param name="comboBox">The combobox.</param>
		/// <param name="intValue">The value to find and set.</param>
		public static void setComboBoxSelection(System.Windows.Forms.ComboBox comboBox,int intValue)
		{
			int comboBoxIndex=-1;
			int count=comboBox.Items.Count;
			int index=0;
			while((index<count)&&(comboBoxIndex==-1))
			{
				LookupValue lvalue=(LookupValue)comboBox.Items[index];
				if(lvalue.IntValue==intValue)
				{
					comboBoxIndex=index;
				}
				else
				{
					index++;
				}
			}
			if(comboBoxIndex>-1)
			{
				comboBox.SelectedIndex=comboBoxIndex;
			}
			else
			{
				comboBox.SelectedText="";
				comboBox.SelectedItem=null;
			}
		}

		/// <summary>
		/// Reads an item.
		/// </summary>
		/// <param name="address">The starting address for the item.</param>
		/// <param name="romIO">The IMegaDriveIO to load values from.</param>
		/// <returns>The item stored at the specified address.</returns>
		public static PS3Item readItem(int address,IMegaDriveIO romIO)
		{
			PS3Item item=new PS3Item();
			int itemValue=romIO.readInteger(address,2);
			//convert to hex
			String hexString=itemValue.ToString("X");
			while(hexString.Length<4)
			{
				hexString="0"+hexString;
			}			
			//set item properties
			item.hexString=hexString;
			item.isEquipped=item.hexString.StartsWith("8");
			int itemCode=int.Parse(item.hexString.Substring(1,2),System.Globalization.NumberStyles.HexNumber);
			item.itemLookup=ItemCodes.getByValue(itemCode);
			if(item.isEquipped)
			{
				int equipCode=int.Parse(item.hexString.Substring(3,1),System.Globalization.NumberStyles.HexNumber);
				item.whereEquipped=EquipCodes.getByValue(equipCode);
			}
			else
			{
				item.whereEquipped=EquipCodes.getByValue(0);
			}
			return(item);
		}

		/// <summary>
		/// Save character equipment back to the ROM.
		/// </summary>
		/// <param name="baseAddress">The base address for the character.</param>
		/// <param name="listView">The ListView to read items from - assumes listview has items in the following order: hex value, item, equipped, equipped where</param>
		/// <param name="romIO">The IMegaDriveIO to use to write.</param>
		public static void saveCharacterEquipment(int baseAddress,ListView listView,IMegaDriveIO romIO)
		{
			int itemCount=listView.Items.Count;
			//write the inventory count
			int itemCountAddress=baseAddress+(int)Constants.CharacterOffsets.ItemCount;
			romIO.writeInt(new MDInteger(itemCountAddress,1,"",(itemCount*2),"",0,0));
			//write the items
			for(int index=0;index<itemCount;index++)
			{
				ListViewItem currentItem=listView.Items[index];
				int itemSlot=(itemCount-index-1)*2;
				int address=(baseAddress+((int)Constants.CharacterOffsets.Items)+(itemSlot));
				saveInventoryItem(currentItem.SubItems[0].Text,address,romIO);
			}
		}

		/// <summary>
		/// Saves an inventory item.
		/// </summary>
		/// <param name="hexString">The hex string of the item being saved.</param>
		/// <param name="address">The address to save at.</param>
		/// <param name="romIO">The IMegaDriveIO to use to write.</param>
		public static void saveInventoryItem(string hexString,int address,IMegaDriveIO romIO)
		{
			MDInteger mdInt=new MDInteger();
			//convert the hex string to an int
			mdInt.CurrentValue=int.Parse(hexString.ToString(),System.Globalization.NumberStyles.HexNumber);
			mdInt.NumBytes=2;
			mdInt.Address=address;
			//now save
			romIO.writeInt(mdInt);
		}

		/// <summary>
		/// Return the first occurance of some text in a ListView.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <param name="column">The column to search in.</param>
		/// <param name="startIndex">The row index to start searching at.</param>
		/// <param name="listView">The ListView to search.</param>
		/// <returns>The row index the text was found at, -1 if not found.</returns>
		public static int findNextInListView(string text,int column,int startIndex,ListView listView)
		{
			string searchText=text.ToLower();
			int rowCount=listView.Items.Count;
			int index=startIndex;
			int foundAt=-1;
			while((index<rowCount)&&(foundAt==-1))
			{
				string currentValue=listView.Items[index].SubItems[column].Text.ToLower().Replace("\xF8"," ").Replace("\xE4"," ");
				if(currentValue.IndexOf(searchText)>-1)
				{
					foundAt=index;				
				}
				else
				{
					index++;
				}
			}
			return(foundAt);
		}

		/// <summary>
		/// Return the first occurance of some text in a ListView.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <param name="column">The column to search in.</param>
		/// <param name="startIndex">The row index to start searching at.</param>
		/// <param name="listView">The ListView to search.</param>
		/// <returns>The row index the text was found at, -1 if not found.</returns>
		public static int findPreviousInListView(string text,int column,int startIndex,ListView listView)
		{
			string searchText=text.ToLower();
			int rowCount=listView.Items.Count;
			int index=startIndex;
			int foundAt=-1;
			while((index>0)&&(foundAt==-1))
			{
				string currentValue=listView.Items[index].SubItems[column].Text.ToLower().Replace("\xF8"," ").Replace("\xE4"," ");
				if(currentValue.IndexOf(searchText)>-1)
				{
					foundAt=index;				
				}
				else
				{
					index--;
				}
			}
			return(foundAt);
		}

		/// <summary>
		/// Find the index of a technique power in Constants.TechniquePowers.
		/// </summary>
		/// <param name="techniquePower">The technique power to find.</param>
		/// <returns>The index where techniquePower is located.</returns>
		public static int findTechniquePowerIndex(int techniquePower)
		{
			int foundIndex=-1;
			int currentIndex=0;
			int length=Constants.TechniquePowers.Length;
			while((foundIndex<0)&&(currentIndex<length))
			{
				if(Constants.TechniquePowers[currentIndex]==techniquePower)
				{
					foundIndex=currentIndex;
				}
				currentIndex++;
			}
			return(foundIndex);
		}


		/// <summary>
		/// Convert a percent value in the rom to something meaningful to a user.
		/// </summary>
		/// <param name="romPercent">The percent value stored in the rom, between 0 and 255.</param>
		/// <returns>A number between 0 and 100.</returns>
		public static int percentFromRomToDecimal(int romPercent)
		{
			return((int)Math.Round(((double)romPercent/256D)*100D));
		}

		/// <summary>
		/// Convert a percent entered by a user into something that can be stored in the rom.
		/// </summary>
		/// <param name="percentDecimal">A number between 0 and 100.</param>
		/// <returns>The equivalent value to store in the rom, between 0 and 255.</returns>
		public static int percentFromDecimalToRom(int percentDecimal)
		{
			return((int)Math.Round(((double)percentDecimal/100D)*256D));
		}

		/// <summary>
		/// see http://support.microsoft.com/kb/319401
		/// </summary>
		/// <param name="e"></param>
		/// <param name="listView">The ListView to sort.</param>
		public static void sortListView(ColumnClickEventArgs e,ListView listView)
		{
			ListViewColumnSorter sorter=(ListViewColumnSorter)listView.ListViewItemSorter;
			if(e.Column==sorter.SortColumn )
			{
				if(sorter.Order==SortOrder.Ascending)
				{
					sorter.Order=SortOrder.Descending;
				}
				else
				{
					sorter.Order=SortOrder.Ascending;
				}
			}
			else
			{
				sorter.SortColumn=e.Column;
				sorter.Order=SortOrder.Ascending;
			}
			// Perform the sort with these new sort options.
			listView.Sort();
		}
	}
}
