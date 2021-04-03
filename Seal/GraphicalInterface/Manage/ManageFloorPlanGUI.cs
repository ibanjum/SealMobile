using Urho;
using Urho.Gui;
using Urho.Resources;

namespace Seal.GraphicalInterface.Manage
{
    public partial class ManageFloorPlanGUI : Application
    {
        UIElement uiRoot;
        public Scene scene;
        Node CameraNode;
        Viewport viewport;

        public ManageFloorPlanGUI(ApplicationOptions options) : base(options) { }

        protected override void Start()
        {
            base.Start();

            uiRoot = UI.Root;

            var cache = ResourceCache;
            XmlFile defaultStyle = cache.GetXmlFile("UI/DefaultStyle.xml");

            uiRoot.SetDefaultStyle(defaultStyle);
            scene = new Scene();
        }

        public void CreatCameraAndPort()
        {
            CameraNode = scene.CreateChild("Camera");
            CameraNode.Position = (new Vector3(0.0f, 0.0f, 0.0f));
            CameraNode.CreateComponent<Camera>().Orthographic = true;

            viewport = new Viewport(Context, scene, CameraNode.GetComponent<Camera>(), null);
            viewport.SetClearColor(Color.White);
            Renderer.SetViewport(0, viewport);
        }
    }
}
