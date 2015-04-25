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

namespace com.huguesjohnson.aridia.ui
{
	/// <summary>
	/// Contains constants and enumerations used by Aridia.
	/// Externalizing these would probably make sense if there was ever any desire to make this application support other ROM variants (right now there isn't).
	/// </summary>
	public abstract class Constants
	{
		/// <summary>
		/// How technique power is stored in the ROM.
		/// </summary>
		public static readonly int[] TechniquePowers=new int[]{0,514,771,1028,1285,1542};
		
		/// <summary>
		/// Order of techniques in the ROM.
		/// </summary>
		public enum TechniqueGroupOrder : int
		{
			Melee=0,
			Heal=1,
			Time=2,
			Order=3
		}

		/// <summary>Number of techniques in a technique group.</summary>
		public const int TechniqueGroupSize=4;
		
		/// <summary>
		/// Where various tiles are stored.
		/// </summary>
		public enum TileAddresses : int
		{
			LogoStart=425984,
			LogoEnd=429055,
			FontStart=417824,
			FontEnd=421727,
			BorderStart=422592,
			BorderEnd=422911,
			CreditFontStart=422912,
			CreditFontEnd=423743,
			StatusFontStart=422400,
			StatusFontEnd=422561
		}

		/// <summary>
		/// Where item attributes are located relative to their starting address.
		/// </summary>
		public enum ItemOffsets : int
		{
			NameAddress=4,
			Cost=6,
			Animation=8,
			Attack=10,
			Defense=11,
			Speed=12,
			Technique=13,
			Effectiveness=14,
			EquipBy=15,
			Type=16,
			Droppable=17
		}

		/// <summary>Location where item names start.</summary>
		public const int ItemNameStartAddress=234050;

		/// <summary>
		/// Where weapon attributes are located relative to their starting address.
		/// </summary>
		public enum WeaponOffsets : int
		{
			Cost=6,
			Animation=8,
			Attack=10,
			Defense=11,
			Speed=12,
			Technique=13,
			EquipBy=15
		}

		/// <summary>
		/// Character IDs used for equip bitmask, taken from ps3.constants.asm by lorenzo
		/// </summary>
        public enum CharacterIdMask : int
        { 
            RhysNial=1,
            AynSeanCrysAdanAron=2,
            Mieu=4,
            Wren=8,
            TheaKara=16,
            LyleRyan=32,
            LenaSari=64,
            LayaGwyn=128
        }

		/// <summary>
		/// Where character attributes are located relative to their starting address.
		/// </summary>
		public enum CharacterOffsets : int
		{
			Type=0,
			HitPoints=1,
			TechniquePoints=2,
			Attack=3,
			Defense=4,
			Speed=5,
			Name=7,
			Luck=12,
			Skill=13,
			LevelTemplate=14,
			TechniquePower=16,
			ItemCount=19,
			Items=20
		}

		/// <summary>
		/// Where enemy attributes are located relative to their starting address.
		/// </summary>
		public enum EnemyOffsets : int
		{
			SpriteGroup=1,
			Animation=5,
			NameLookup=7,
			HitPoints=9,
			Technique=11,
			TechniqueLevel=13,
			TechniqueCastPercent=14,
			Attack=15,
			Defense=17,
			Speed=19,
			EscapePercent=20,
			Experience=21,
			Meseta=23
		}

		/// <summary>
		/// Where level table attributes are located relative to their starting address.
		/// </summary>
		public enum LevelTableOffsets : int
		{
			HP=0,
			TP=1,
			Damage=2,
			Defense=3,
			Speed=4,
			Luck=5,
			Skill=6,
			XPTable=10
		}

		/// <summary>Address where enemy names start.</summary>
		public const int EnemyNameOffset=245268;

         /// <summary>
		/// Where treause attributes are located relative to their starting address.
		/// </summary>
		public enum TreasureOffsets : int
		{
			X=0,
			Y=2,
			ItemCode=4
		}
    }
}