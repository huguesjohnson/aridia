/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2018 Hugues Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;

namespace com.huguesjohnson.aridia.ui
{
	/// <summary>
	/// Contains constants and enumerations used by Aridia.
	/// Externalizing these would probably make sense if there was ever any desire to make this application more generic (right now there isn't).
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
			Cost=6,
			Technique=13,
			Effectiveness=14
		}

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
	}
}
