/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2021 Hugues Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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
