using System.Collections.Generic;
using Seal.Models;
using Urho;
using Urho.Gui;
using Urho.Physics;
using Urho.Resources;
using Urho.Urho2D;

namespace Seal.GraphicalInterface.UpdateEdit
{
    public partial class FloorPlanGUI : Application
    {
        UIElement uiRoot;
        public Scene scene;
        Node CameraNode;

        Node pickedNode;
        Viewport viewport;
        Node ItemNode;
        Node anchorContainer;

        Material lineMaterialTransparent;
        Material lineMaterialVisible;

        bool ZoomIn;
        bool ZoomOut;
        bool Rotate;
        bool Delete;
        bool Drag;

        RigidBody2D otherBody;
        CollisionEdge2D CollisionEdgeHit;
        CollisionData[] collisionData;
        IReadOnlyList<Component> components;
        Font font;

        List<CategoryModel> Categories;
        string[] tableItems = new string[2] { "test11", "roundtable" };
        string[] bathroomItems = new string[2] { "bathroom", "wall" };

        public FloorPlanGUI(ApplicationOptions options) : base(options) { }

        protected override void Start()
        {
            base.Start();
            uiRoot = UI.Root;
            Input.SetMouseVisible(true, false);

            font = ResourceCache.GetFont("Fonts/Anonymous Pro.ttf");

            var cache = ResourceCache;
            XmlFile defaultStyle = cache.GetXmlFile("UI/DefaultStyle.xml");

            uiRoot.SetDefaultStyle(defaultStyle);

            Categories = new List<CategoryModel>()
            {
                new CategoryModel("Tables")
                {
                    CategoryItems = new List<ItemModel>()
                    {
                        new ItemModel("test11") { NumberOfSeats = 4 },
                        new ItemModel("roundtable")
                    }
                },
                new CategoryModel("Walls")
                {
                    CategoryItems = new List<ItemModel>()
                    {
                        new ItemModel("wall")
                    }
                },
                new CategoryModel("Bathrooms")
                {
                    CategoryItems = new List<ItemModel>()
                    {
                        new ItemModel("bathroom")
                    }
                }
            };

            CreateScene();
            SetupViewport();
            SubscribeToEvents();

            CreateButtons();
        }
        void SubscribeToEvents()
        {
            Input.TouchBegin += HandleTouchBegin;
        }

        void SetupViewport()
        {
            var renderer = Renderer;
            viewport = new Viewport(Context, scene, CameraNode.GetComponent<Camera>(), null);
            viewport.SetClearColor(Color.White);
            renderer.SetViewport(0, viewport);
        }
    }
}