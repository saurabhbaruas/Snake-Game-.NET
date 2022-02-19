using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    class Input
    {
        //Load list of available Keyboard butons
        private static Hashtable keyTable = new Hashtable();

        //Perform aa check to se if a particuler buton is paased.
        public static bool KeyPressed(Keys key)
        {
            if(keyTable[key] == null)
            {
                return false;
            }
            return (bool)keyTable[key];

        }
        //detect if a keybord button is passed.
        public static void ChangeState(Keys key, bool state)
        {
            keyTable[key] = state;
        }
    }
}
