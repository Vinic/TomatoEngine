using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    public static class PhysEngine
    {
        private static List<RenderObject> _tempList = new List<RenderObject>();
        //handles collision
        public static void Collide(RenderObject physObject1, RenderObject physObject2)
        {
            PointFloat vel1 = physObject1.GetVelocity();
            PointFloat vel2 = physObject2.GetVelocity();
            while (!IsOverlappingInCircle(physObject1, physObject2))
            {
                physObject1.SetPosAdd(-vel1.x, -vel1.y);
            }
            float rfx = vel1.x - vel2.x;
            float rfy = vel1.y - vel2.y;
            PointFloat pos1 = physObject1.GetPosition();
            PointFloat pos2 = physObject2.GetPosition();
            float x = pos1.x - pos2.x;
            float y = pos1.y - pos2.y;
            float h = (float)Math.Atan2(x,y);
            //e = (e / 360f) * (float)Math.PI * 2f;
            PointFloat dir = GetDirection(-h);
            physObject1.SetVelocityAdd(dir.x * ((vel2.x + vel1.x) / 2), dir.y * ((vel2.y + vel1.y) / 2));
            physObject2.SetVelocityAdd(-dir.x * ((vel2.x + vel1.x) / 2), -dir.y * ((vel2.y + vel1.y) / 2));
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
            float physSize = physObject.GetSize();
            foreach (RenderObject obj in TomatoMainEngine.GameObjects)
            {
                if (obj.HasPhysics() && obj != physObject)
                {
                    PointFloat objPos = obj.GetPosition();
                    float gemX = Math.Abs(objPos.x - physPos.x);
                    float gemY = Math.Abs(objPos.y - physPos.y);
                    float fDistance = gemX + gemY - obj.GetSize();
                    if (fDistance < physSize)
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
            float physSize = physObject1.GetSize();

            if (physObject2.HasPhysics() && physObject2 != physObject1)
            {
                PointFloat objPos = physObject2.GetPosition();
                float gemX = Math.Abs(objPos.x - physPos.x);
                float gemY = Math.Abs(objPos.y - physPos.y);
                float fDistance = gemX + gemY - physObject2.GetSize();
                if (fDistance < physSize)
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
            float physSize = physObject.GetSize();
            foreach (RenderObject obj in TomatoMainEngine.GameObjects)
            {
                if (obj.HasPhysics() && obj != physObject)
                {
                    PointFloat objPos = obj.GetPosition();
                    float gemX = Math.Abs(objPos.x - physPos.x);
                    float gemY = Math.Abs(objPos.y - physPos.y);
                    float fDistance = gemX + gemY - obj.GetSize();
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
