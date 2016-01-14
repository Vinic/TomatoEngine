using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomatoEngine
{
    static class ControlKeys
    {
        public static List<Keys> Downs = new List<Keys>();
        public static List<Keys> Ups = new List<Keys>();
        public static KeysConverter keyC = new KeysConverter();
        public static void KeyDown(Keys key){
            if(!Downs.Contains(key)){
                Downs.Add(key);
            }
        }
        public static void KeyUp(Keys key)
        {
            bool isDown = false;
            foreach(Keys down in Downs){
                if(down == key){
                    isDown = true;
                }
            }
            if (isDown)
            {
                Downs.Remove(key);
                Ups.Add(key);
            }
        }
        public static bool IsKeyDown(string key)
        {
            bool isDown = false;
            Keys k = (Keys)System.Enum.Parse(typeof(Keys), key.ToUpper());
            foreach (Keys down in Downs)
            {
                if (down == k)
                {
                    isDown = true;
                    
                }
            }
            return isDown;
        }
        public static bool IsKeyPressed(Keys key)
        {
            bool isUp = false;
            foreach ( Keys down in Ups )
            {
                if ( down == key )
                {
                    isUp = true;
                }
            }
            if ( Ups.Count > 0 && isUp )
            {
                Ups.Clear();
            }
            return isUp;
        }
        public static bool IsKeyPressed(string key)
        {
            Keys k = (Keys)System.Enum.Parse(typeof(Keys), key.ToUpper());
            return IsKeyPressed(k);
        }
    }
}
