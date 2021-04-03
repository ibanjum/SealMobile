using Urho.Resources;
using Urho;
using Urho.Actions;
using Urho.Gui;
using Urho.IO;
using Urho.Urho2D;

namespace Seal.GraphicalInterface.UpdateEdit
{
    public partial class FloorPlanGUI
    {
        void CreateScene()
        {
            scene = new Scene();



            var dir = FileSystem.UserDocumentsDir;
            //System.IO.File.Delete(dir + "FloorPlan.xml");

            if (System.IO.File.Exists(dir + "FloorPlan.xml"))
            {
                scene.LoadXml(dir + "FloorPlan.xml");
                EnvironmentNode = scene.GetChild("EvironmentNode");
            }
            else
            {
                scene.CreateComponent<Octree>();
                scene.CreateComponent<PhysicsWorld2D>();

                scene.GetComponent<PhysicsWorld2D>().PhysicsBeginContact2D += HandleCollisionBegin;
                scene.GetComponent<PhysicsWorld2D>().PhysicsEndContact2D += HandleCollisionEnd;
                scene.GetComponent<PhysicsWorld2D>().ContinuousPhysics = true;

                lineMaterialVisible = new Material();
                lineMaterialVisible.SetTechnique(0, CoreAssets.Techniques.NoTextureUnlitVCol, 1, 1);

                lineMaterialTransparent = new Material();
                lineMaterialTransparent.SetTechnique(0, CoreAssets.Techniques.NoTextureAdd, 1, 1);

                EnvironmentNode = scene.CreateChild("EvironmentNode");
                EnvironmentNode.SetDeepEnabled(true);
            }

            CameraNode = scene.CreateChild("Camera");
            CameraNode.Position = (new Vector3(0.0f, 0.0f, 0.0f));
            CameraNode.CreateComponent<Camera>().Orthographic = true;
        }

        void CreateItem(UIElement item)
        {

            pickedNode = EnvironmentNode.CreateChild(item.Name + "PlanComponent");

            pickedNode.Position = new Vector3(0f, 0f, 0f);
            pickedNode.Scale = new Vector3(0.5f, 0.5f, 0.0f);

            pickedNode.CreateComponent<RigidBody2D>();
            pickedNode.GetComponent<RigidBody2D>().BodyType = BodyType2D.Dynamic;
            pickedNode.GetComponent<RigidBody2D>().GravityScale = 0;

            pickedNode.CreateComponent<StaticSprite2D>();
            pickedNode.GetComponent<StaticSprite2D>().OrderInLayer = -1;
            pickedNode.GetComponent<StaticSprite2D>().Sprite = ResourceCache.GetSprite2D("Images/" + item.Name + ".png");

            pickedNode.RunActionsAsync(new TintTo(0.2f, 0.0f, 1.0f, 0.8f));
            toolContainer.SetDeepEnabled(true);

            CollisionBox2D shape = pickedNode.CreateComponent<CollisionBox2D>();
            shape.Trigger = true;
            shape.Size = new Vector2(6f, 6f);


            /*CustomGeometry geom = pickedNode.CreateComponent<CustomGeometry>();
            geom.BeginGeometry(0, PrimitiveType.LineList);
            geom.SetMaterial(lineMaterialVisible);

            AddLines(pickedNode, geom);*/

        }

        void AddLines(Node containerNode, CustomGeometry geom)
        {
            float size = 2.56f;

            Vector2 start0 = new Vector2(containerNode.Position.X + size, -50);
            Vector2 end0 = new Vector2(containerNode.Position.X + size, 50);
            CreateVertice(containerNode, geom, start0, end0, 0);

            Vector2 start1 = new Vector2(containerNode.Position.X - size, -50);
            Vector2 end1 = new Vector2(containerNode.Position.X - size, 50);
            CreateVertice(containerNode, geom, start1, end1, 2);

            Vector2 start2 = new Vector2(-50, containerNode.Position.Y + size);
            Vector2 end2 = new Vector2(50, containerNode.Position.Y + size);
            CreateVertice(containerNode, geom, start2, end2, 4);

            Vector2 start3 = new Vector2(-50, containerNode.Position.Y - size);
            Vector2 end3 = new Vector2(50, containerNode.Position.Y - size);
            CreateVertice(containerNode, geom, start3, end3, 6);

            geom.Commit();

        }

        void CreateVertice(Node pickedNode, CustomGeometry geom, Vector2 start, Vector2 end, int id)
        {
            geom.DefineVertex(new Vector3(start.X, start.Y, 0));
            geom.DefineColor(Color.White);
            geom.DefineVertex(new Vector3(end.X, end.Y, 0));
            geom.DefineColor(Color.White);

            CollisionEdge2D edge2D = pickedNode.CreateComponent<CollisionEdge2D>();
            edge2D.Trigger = true;
            edge2D.GroupIndex = id;
            edge2D.SetVertices(start, end);
        }
    }
}
