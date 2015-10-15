﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomatoEngine
{
    static class ControlKeys
    {
        public static List<Keys> Downs = new List<Keys>();
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
            }
        }
        public static bool IsKeyDown(string key)
        {
            bool isDown = false;
            Keys k = (Keys)keyC.ConvertFromString(key);
            foreach (Keys down in Downs)
            {
                if (down == k)
                {
                    isDown = true;
                }
            }
            return isDown;
        }
    }
}