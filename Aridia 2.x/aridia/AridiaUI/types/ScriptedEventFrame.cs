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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using com.huguesjohnson.aridia.ui;

namespace com.huguesjohnson.aridia.types
{
    /// <summary>
    /// Extremely simply class to transport the indivual frames in a scripted event.
    /// </summary>
    public class ScriptedEventFrame 
    { 
        private int _frameNumber;
        public int frameNumber
        {
            get{return(_frameNumber);}
            set{_frameNumber=value;}
        }

        private int _byte1;
        public int byte1
        {
            get{return(_byte1);}
            set{_byte1=value;}
        }

        private int _byte2;
        public int byte2
        {
            get{return(_byte2);}
            set{_byte2=value;}
        }
        
        public override string ToString()
        {
            StringBuilder tostring=new StringBuilder();
            tostring.Append("[");
            if(this.frameNumber<10){tostring.Append("0");}
            tostring.Append(frameNumber);
            tostring.Append("] - ");
            if(byte1==0)
            {
                tostring.Append("Dialog[");
                tostring.Append(byte2);
                tostring.Append("]");
            }
            else
            {
                switch(byte2)
                {
                    case 0: tostring.Append("Delay["); break;
                    case (int)Constants.ButtonsMasks.Up: tostring.Append("Up["); break;
                    case (int)Constants.ButtonsMasks.Down: tostring.Append("Down["); break;
                    case (int)Constants.ButtonsMasks.Left: tostring.Append("Left["); break;
                    case (int)Constants.ButtonsMasks.Right: tostring.Append("Right["); break;
                    case (int)Constants.ButtonsMasks.A: tostring.Append("A["); break;
                    case (int)Constants.ButtonsMasks.B: tostring.Append("B["); break;
                    case (int)Constants.ButtonsMasks.C: tostring.Append("C["); break;
                    case (int)Constants.ButtonsMasks.Start: tostring.Append("Start["); break;
                }
                tostring.Append(byte1);
                tostring.Append("]");
            }
            return(tostring.ToString());
        }
    }

}