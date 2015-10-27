using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    public static class PhysEngine
    {
        //handles collision
        public static void Collide(RenderObject physObject1, RenderObject physObject2)
        {
            
        }
        //is colliding
        public static bool IsOverlappingInCircle(RenderObject physObject)
        {
            foreach (RenderObject obj in TomatoMainEngine.GameObjects)
            {
                if(obj != physObject) {
                    float gemX = Math.Abs(obj.GetPosition().x - physObject.GetPosition().x);
                    float gemY = Math.Abs(obj.GetPosition().y - physObject.GetPosition().y);
                    //Console.WriteLine("X: " + gemX + " " + "Y: " + gemY);

                    float fDistance = (gemX + gemY) / 2;

                    if (fDistance < physObject.GetSize())
                    {
                        return true;
                    }
                }
            }
            return false;
        } 
    }
}
