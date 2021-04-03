using Urho;
using Urho.Urho2D;

namespace Seal.GraphicalInterface.UpdateEdit
{
    public partial class FloorPlanGUI
    {
        bool locked;
        Vector3 direc;
        void HandleCollisionBegin(PhysicsBeginContact2DEventArgs args)
        {
            if (pickedNode != null)
            {

                /* foreach (var contact in args.Contacts)
                 {
                     var vector = pickedNode.Position;
                     var normal = contact.ContactNormal;

                     var right = new Vector3(normal.Y, -normal.X, 0); //in world space
                     var forward = new Vector3(0, -normal.Z, normal.Y); //in world space
                     right.Normalize();
                     forward.Normalize();

                     //the direction the player will move (tangential), which is a combination of any two non-parallel vectors on the correct plane
                     direc = right * vector.X + forward * vector.Z;
                     direc.Normalize();

                     locked = true;
                 }*/

                /*if (args.BodyB == pickedNode.GetComponent<RigidBody2D>())
                {
                    if (args.ShapeA.TypeName == CollisionEdge2D.TypeNameStatic)
                    {
                        UpdateLineAndBody((CollisionEdge2D)args.ShapeA, args.BodyA, Color.Black);
                    }
                    else if (args.ShapeB.TypeName == CollisionEdge2D.TypeNameStatic)
                    {
                        UpdateLineAndBody((CollisionEdge2D)args.ShapeB, args.BodyB, Color.Black);
                    }
                }*/
            }
        }
        void HandleCollisionEnd(PhysicsEndContact2DEventArgs args)
        {
            /*if (args.ShapeA.TypeName == CollisionEdge2D.TypeNameStatic)
            {
                UpdateLineAndBody((CollisionEdge2D)args.ShapeA, args.BodyA, Color.White);
            }
            else if (args.ShapeB.TypeName == CollisionEdge2D.TypeNameStatic)
            {
                UpdateLineAndBody((CollisionEdge2D)args.ShapeB, args.BodyB, Color.White);
            }*/
        }

        void UpdateLineAndBody(CollisionEdge2D collisionEdge, RigidBody2D other, Color color)
        {
            if (collisionEdge.Node != null)
            {
                CustomGeometry geometry = collisionEdge.Node.GetComponent<CustomGeometry>();
                uint id = (uint)collisionEdge.GroupIndex;
                unsafe
                {
                    var line = geometry.GetVertex(0, id);
                    (*line).Color = color.ToUInt();
                    var line1 = geometry.GetVertex(0, id + 1);
                    (*line1).Color = color.ToUInt();
                    geometry.Commit();
                }
                otherBody = other;
                CollisionEdgeHit = collisionEdge;
            }
        }
    }
    public enum CollisionSide
    {
        Left,
        Right,
        Top,
        Bottom
    }
}
