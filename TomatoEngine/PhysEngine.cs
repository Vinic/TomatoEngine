using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    
    public static class PhysEngine
    {
        public static int PhysInteractions = 0;
        private static List<RenderObject> _tempList = new List<RenderObject>();
        //handles collision
        public static void Collide(RenderObject physObject1, RenderObject physObject2)
        {
            PointFloat vel1 = physObject1.GetVelocity();
            PointFloat vel2 = physObject2.GetVelocity();
            PointFloat pos1 = physObject1.GetPosition();
            PointFloat pos2 = physObject2.GetPosition();
            float x = pos1.x - pos2.x;
            float y = pos1.y - pos2.y;
            float h = -(float)Math.Atan2(x,y);
            PointFloat dir = GetDirection(h + (float)Math.PI / 2);
            
            float e1 = Math.Abs(vel1.x + vel2.x + 0.001f) * dir.x;
            float e2 = Math.Abs(vel1.y + vel2.y + 0.001f) * dir.y;
            if ( physObject2.HasMass()  == true)
            {
                physObject1.SetPosAdd(-vel1.x, -vel1.y);
                physObject1.SetVelocity(e1, e2);
                physObject1.SetRotationVelocity(( (float)Math.Atan2(vel1.x + vel2.x, vel1.y + vel2.y) - (float)Math.Atan2(x, y) ) / 100f);
            }
            if( physObject1.HasMass() == true){
                physObject2.SetVelocity(-e1, -e2);
                physObject2.SetRotationVelocity(( (float)Math.Atan2(vel1.x + vel2.x, vel1.y + vel2.y) - (float)Math.Atan2(x, y) ) / 100f);
            }
            PhysInteractions++;
            
        }

        public static void HandleAllObjects( RenderObject physObject, List<RenderObject> objectList)
        {
            foreach (RenderObject obj in objectList)
            {
                Collide(physObject, obj);
            }
        }

        //is colliding
        public static bool IsOverlappingInCircle(RenderObject physObject)
        {
            PointFloat physPos = physObject.GetPosition();
            float physSize = physObject.GetPhysSize();
            foreach (RenderObject obj in TomatoMainEngine.GameObjects)
            {
                if (obj.HasPhysics() && obj != physObject)
                {
                    PointFloat objPos = obj.GetPosition();
                    float gemX = Math.Abs(objPos.x - physPos.x);
                    float gemY = Math.Abs(objPos.y - physPos.y);
                    float fDistance = gemX + gemY;
                    fDistance = (float)(Math.Pow(physPos.x-objPos.x, 2) + Math.Pow(physPos.y-objPos.y, 2));
                    if (fDistance < physSize + obj.GetPhysSize())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool IsOverlappingInCircle(RenderObject physObject1, RenderObject physObject2)
        {
            PointFloat physPos = physObject1.GetPosition();
            float physSize = physObject1.GetPhysSize();

            if (physObject2.HasPhysics())
            {
                PointFloat objPos = physObject2.GetPosition();
                float gemX = Math.Abs(objPos.x - physPos.x);
                float gemY = Math.Abs(objPos.y - physPos.y);
                float fDistance = gemX + gemY;
                if ( fDistance < physSize + physObject2.GetPhysSize())
                {
                    return true;
                }
            }
            return false;
        }
        public static List<RenderObject> GetAllOverlappingInCircle(RenderObject physObject)
        {
            _tempList.Clear();
            PointFloat physPos = physObject.GetPosition();
            float physSize = physObject.GetPhysSize();
            foreach (RenderObject obj in TomatoMainEngine.GameObjects)
            {
                if (obj.HasPhysics() && obj != physObject)
                {
                    PointFloat objPos = obj.GetPosition();
                    float gemX = Math.Abs(objPos.x - physPos.x);
                    float gemY = Math.Abs(objPos.y - physPos.y);
                    float fDistance = gemX + gemY - obj.GetPhysSize();
                    if (fDistance < physSize)
                    {
                        _tempList.Add(obj);
                    }
                }
            }
            return _tempList;
        }

        private static PointFloat GetDirection(float deg)
        {
            return new PointFloat( (float)Math.Cos(deg) , (float)Math.Sin(deg) );
        }
    }
}
