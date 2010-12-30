/*
MegaDriveIO: Utilities to read/write a Mega Drive binary ROM image
Originally created for Aridia: Phantasy Star III ROM Editor
Modified for Eisfrei: Herzog Zwei ROM Editor
Copyright (c) 2007-2009 Hugues Johnson

MegaDriveIO is free software; you can redistribute it and/or modify
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
using System.IO;

namespace com.huguesjohnson.aridia.MegaDriveIO
{
	/// <summary>
	/// Used to read/write a MegaDrive binary ROM image.
	/// </summary>
	public class MDBinaryRomIO : IDisposable
	{
		private const int CHECKSUM_ADDRESS=398;
		private const int CHECKSUM_CALC_START=512;
		//this next value is true for Phantasy Star III, if this is expanded to support other games may need to re-evaluate it
		private const byte STRING_TERMINATOR_BYTE=(byte)252;
        private BinaryWriter writer;
		private BinaryReader reader;

		/// <summary>
		/// Sets the position of a file stream.
		/// </summary>
		/// <param name="stream">The stream to update.</param>
		/// <param name="address">The new position for the stream.</param>
		private void setStreamPosition(Stream stream,int address)
		{
			long position=stream.Position;
			if(address>position)
			{
				stream.Seek(address-position,SeekOrigin.Current);
			}
			else if(address<position)
			{
				stream.Seek(address,SeekOrigin.Begin);
			}		
		}

		/// <summary>
		/// Create a new instance with the specified file.
		/// </summary>
		/// <param name="filePath">The full path to the file to edit.</param>
		public MDBinaryRomIO(String filePath)
		{ 
			FileStream romFileStream=File.Open(filePath,FileMode.Open,FileAccess.ReadWrite);
			this.writer=new BinaryWriter(romFileStream);
			this.reader=new BinaryReader(romFileStream);
		}

		/// <summary>
		/// Write a string value to the binary rom image.
		/// </summary>
		/// <param name="mdString">The value to write.</param>
		public void writeString(MDString mdString)
		{
			//move to the correct position
			this.setStreamPosition(this.writer.BaseStream,mdString.Address);
			//convert to bytes
			char[] chars=mdString.CurrentValue.ToCharArray();
			int length=chars.Length;
			byte[] bytes=new byte[length];
			for(int index=0;index<length;index++)
			{
				bytes[index]=(byte)chars[index];
			}
			//write the bytes
			this.writer.Write(bytes,0,length);
			//write the string terminator
			this.writer.Write(STRING_TERMINATOR_BYTE);
		}

		/// <summary>
		/// Write an integer value to the binary rom image.
		/// </summary>
		/// <param name="mdInt">The value to write.</param>
		public void writeInt(MDInteger mdInt)
		{
			//move to the correct position
			this.setStreamPosition(this.writer.BaseStream,mdInt.Address);
			/*
			 * OK, yes this is converting the number to hex before writing it.
			 * This is purely done to make debugging easier. Set a breakpoint at the next for loop to see how
			 * Sure, it could perform better but it's not like we're running millions of transactions through it.
			 */
			String hexString=mdInt.CurrentValue.ToString("X");
			int length=hexString.Length;
			int expectedLength=mdInt.NumBytes*2;
			while(length<expectedLength)
			{
				hexString="0"+hexString;
				length++;
			}
			for(int index=0;index<length;index+=2)
			{
				String substring=hexString.Substring(index,2);
				byte b=Convert.ToByte(substring,16);
				this.writer.Write(b);
			}
		}

		/// <summary>
		/// Generate the checksum for a binary rom image.
		/// Based off Gens Calculate_Checksum method in rom.c (GPL code - see http://sourceforge.net/projects/gens/)
		/// </summary>
		/// <returns>The new checksum value.</returns>
		public int generateChecksum()
		{
			//move to the correct position
			this.setStreamPosition(this.reader.BaseStream,CHECKSUM_CALC_START);
			//compute the new checksum
			ushort newChecksum=0;
			long length=this.reader.BaseStream.Length;
			for(int index=CHECKSUM_CALC_START;index<length;index+=2)
			{
				newChecksum+=(ushort)(this.reader.ReadByte()<<8);
				newChecksum+=this.reader.ReadByte();
			}
			return(newChecksum);
		}

		/// <summary>
		/// Read a string from the rom image.
		/// </summary>
		/// <param name="offset">The address to start reading at.</param>
		/// <param name="length">The length of the string (number of bytes) to read.</param>
		/// <returns>An string representation of the data read.</returns>
		public string readString(int offset,int length)
		{
			//move to the correct position
			this.setStreamPosition(this.reader.BaseStream,offset);
			//now read the data
			byte[] buffer=new byte[length];
			this.reader.Read(buffer,0,length);
			//convert to char[]
			char[] chars=new char[length];
			int index=0;
			while((index<length)&&(buffer[index]!=STRING_TERMINATOR_BYTE))
			{
				chars[index]=(char)buffer[index];
				index++;
			}
			//return the final string
			return(new String(chars,0,index));
		}

		/// <summary>
		/// Read an integer from the rom image.
		/// </summary>
		/// <param name="offset">The address to start reading at.</param>
		/// <param name="length">The length of the integer (number of bytes) to read.</param>
		/// <returns>An integer representation of the data read.</returns>
		public int readInteger(int offset,int length)
		{
			//move to the correct position
			this.setStreamPosition(this.reader.BaseStream,offset);
			/*
			 * OK, yes this is also converting the number to hex before writing it.
			 * Also purely done to make debugging easier. 
			 */
			System.Text.StringBuilder hexString=new System.Text.StringBuilder(length*2);
			for(int index=0;index<length;index++)
			{
				byte byteValue=this.reader.ReadByte();
				string hexValue=byteValue.ToString("X");
				if(hexValue.Length<2){ hexValue="0"+hexValue; }
				hexString.Append(hexValue);
			}
			return(int.Parse(hexString.ToString(),System.Globalization.NumberStyles.HexNumber));
		}


		/// <summary>
		/// Read raw bytes from the rom image. 
		/// Added for Eisfrei because some text was not stored in ASCII so a custom byte->string translation was needed.
		/// </summary>
		/// <param name="offset">The address to start reading at.</param>
		/// <param name="numBytes">The number of bytes to read.</param>
		/// <returns>An array containing the bytes read.</returns>
		public byte[] readBytes(int offset,int numBytes)
		{
			//move to the correct position
			this.setStreamPosition(this.reader.BaseStream,offset);
			//now read the data
			byte[] buffer=new byte[numBytes];
			this.reader.Read(buffer,0,numBytes);
			//return the buffer
			return(buffer);
		}

		/// <summary>
		/// Write raw bytes from the rom image. 
		/// Added for Eisfrei because some text was not stored in ASCII so a custom byte->string translation was needed.
		/// </summary>
		/// <param name="offset">The address to start reading at.</param>
		/// <param name="bytes">The bytes to write.</param>
		public void writeBytes(int offset,byte[] bytes)
		{
			//move to the correct position
			this.setStreamPosition(this.writer.BaseStream,offset);
			//write the bytes
			int length=bytes.Length;
			this.writer.Write(bytes,0,length);
		}

		/// <summary>
		/// Read a tile from the ROM image.
		/// </summary>
		/// <param name="offset">The address to start at.</param>
		/// <param name="xDimension">The xDimension (width) of the tile.</param>
		/// <param name="yDimension">The yDimension (height) of the tile.</param>
		/// <returns></returns>
		public MDTile readTile(int offset,int xDimension,int yDimension)
		{
			//initialize the tile
			MDTile tile=new MDTile();
			tile.Address=offset;
			tile.PixelData=new byte[xDimension][];
			for(int index=0;index<xDimension;index++)
			{
				tile.PixelData[index]=new byte[yDimension];
			}
			//move to the correct position
			this.setStreamPosition(this.reader.BaseStream,offset);
			//read the tile data - each byte contains *two* pixels
			int size=(xDimension*yDimension)/2;
			byte[] buffer=this.reader.ReadBytes(size);
			int xCounter=0;
			int yCounter=0;
			for(int index=0;index<size;index++)
			{
				byte byteValue=buffer[index];
                //split the byte
				string byteString=byteValue.ToString("X");
				if(byteString.Length<2){ byteString="0"+byteString; }
				tile.PixelData[xCounter][yCounter]=byte.Parse(byteString.Substring(0,1),System.Globalization.NumberStyles.HexNumber);
				xCounter++;
				tile.PixelData[xCounter][yCounter]=byte.Parse(byteString.Substring(1,1),System.Globalization.NumberStyles.HexNumber);
				xCounter++;
				if(xCounter==xDimension)
				{
					xCounter=0; 
					yCounter++;
				}
			}
			return(tile);
		}

		/// <summary>
		/// Saves a set of tiles to the ROM image.
		/// </summary>
		/// <param name="tiles">The set of tiles to save.</param>
		public void writeTiles(MDTile[] tiles)
		{
			int length=tiles.Length;
			for(int index=0;index<length;index++)
			{
				this.writeTile(tiles[index]);
			}
		}

		/// <summary>
		/// Saves a tile to the ROM image.
		/// </summary>
		/// <param name="tile">The tile to save.</param>
		public void writeTile(MDTile tile)
		{
			int address=tile.Address;
			//move to the correct position
			this.setStreamPosition(this.writer.BaseStream,address);
			//this will obvious break if arrays are staggered
			int xDimension=tile.PixelData.Length;
			int yDimension=tile.PixelData[0].Length;
			//recombine the data
			int size=(xDimension*yDimension)/2;
			byte[] buffer=new byte[size];
			int xCounter=0;
			int yCounter=0;
			for(int index=0;index<size;index++)
			{
				string combine=tile.PixelData[xCounter][yCounter].ToString("X")+tile.PixelData[xCounter+1][yCounter].ToString("X");
				buffer[index]=Byte.Parse(combine,System.Globalization.NumberStyles.HexNumber);
				xCounter+=2;
				if(xCounter==xDimension)
				{ 
					xCounter=0; 
					yCounter++;
				}
			}
			//write the buffer back
			this.writer.Write(buffer);
		}

		/// <summary>
		/// Reads palette data from the ROM image.
		/// </summary>
		/// <param name="offset">The address to start at.</param>
		/// <returns>The palette data starting at the specified offset, contains Palette.SIZE entries.</returns>
		public Palette readPalette(int offset)
		{
			Palette palette=new Palette();
			for(int index=0;index<Palette.SIZE;index++)
			{
				int entryValue=this.readInteger(offset+(index*2),2);
				palette.Entries[index]=new PaletteEntry((ushort)entryValue);
			}
			//return the palette
			return(palette);
		}

		/// <summary>
		/// Writes palette data to the ROM image.
		/// </summary>
		/// <param name="palette">The palette data to write, expected to contain Palette.SIZE entries.</param>
		/// <param name="offset">The address to start at.</param>
		public void writePalette(Palette palette,int offset)
		{
			for(int index=0;index<Palette.SIZE;index++)
			{
				MDInteger mdint=new MDInteger();
				mdint.CurrentValue=palette.Entries[index].ToUInt();
				mdint.NumBytes=2;
				mdint.Address=offset+(2*index);
				this.writeInt(mdint);
			}
		}

		/// <summary>
		/// Read an integer from the rom image.
		/// </summary>
		/// <param name="offset">The address to start reading at.</param>
		/// <param name="length">The length of the integer (number of bytes) to read.</param>
		/// <param name="byteOrder">The byte order for the integer.</param>
		/// <returns>An integer representation of the data read.</returns>
		public int readInteger(int offset,int length,ByteOrder byteOrder)
		{
			//move to the correct position
			this.setStreamPosition(this.reader.BaseStream,offset);
			/*
			 * OK, yes this is also converting the number to hex before writing it.
			 * Also purely done to make debugging easier. 
			 */
			System.Text.StringBuilder hexString=new System.Text.StringBuilder(length*2);
			for(int index=0;index<length;index++)
			{
				byte byteValue=this.reader.ReadByte();
				string hexValue=byteValue.ToString("X");
				if(hexValue.Length<2){ hexValue="0"+hexValue; }
				if(byteOrder==ByteOrder.HighByteFirst)
				{
					hexString.Append(hexValue);
				}
				else
				{
					hexString.Insert(0,hexValue);
				}
			}
			return(int.Parse(hexString.ToString(),System.Globalization.NumberStyles.HexNumber));
		}

		#region IDisposable Members

		public void Dispose()
		{
			try{ if(this.writer!=null){ this.writer.Close(); } } catch{ }
			try{ if(this.reader!=null){ this.reader.Close(); } } catch{ }
		}

		#endregion
	}
}
