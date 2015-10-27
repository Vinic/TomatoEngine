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
            PointFloat physPos = physObject.GetPosition();
            foreach (RenderObject obj in TomatoMainEngine.GameObjects)
            {
                if(obj.HasPhysics() && obj != physObject) {
                    PointFloat objPos = obj.GetPosition();
                    float gemX = Math.Abs(objPos.x - physPos.x);
                    float gemY = Math.Abs(objPos.y - physPos.y);
                    //Console.WriteLine("X: " + gemX + " " + "Y: " + gemY);

                    float fDistance = gemX + gemY - obj.GetSize();
                    var s = physObject.GetSize();
                    if (fDistance < s)
                    {
                        return true;
                    }
                }
            }
            return false;
        } 
    }
}
