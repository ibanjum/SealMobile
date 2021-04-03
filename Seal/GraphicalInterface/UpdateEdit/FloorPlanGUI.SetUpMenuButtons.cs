using System;
using Urho;
using Urho.Actions;
using Urho.Gui;
using Urho.Resources;
using Urho.Urho2D;

namespace Seal.GraphicalInterface.UpdateEdit
{
    public partial class FloorPlanGUI
    {
        Node trashButton;
        Node buttonRotate;
        Node toolContainer;
        public Button SaveButton;

        void CreateButtons()
        {
            CreateUIButton("plan_icon", Graphics.Width - 110, 40, 80, 80, Plan_Pressed);
            CreateUIButton("clone", Graphics.Width - 110, 160, 80, 80, Clone_Pressed);
            SaveButton = CreateUIButton("done_button", Graphics.Width - 230, Graphics.Height - 150, 200, 100, null);

            toolContainer = scene.CreateChild();
            buttonRotate = CreateNodeButton(toolContainer, "setting", 0, -7, 0.4f);
            CreateNodeButton(toolContainer, "plus", buttonRotate.Position.X + 2.5f, buttonRotate.Position.Y, 0.2f);
            CreateNodeButton(toolContainer, "minus", buttonRotate.Position.X - 2.5f, buttonRotate.Position.Y, 0.2f);

            trashButton = CreateNodeButton(scene, "trash", 0, -8, 0f);
            trashButton.Enabled = false;
        }

        Button CreateUIButton(string name, int xPos, int yPos, int width, int height, Action<PressedEventArgs> action)
        {
            Button uiButton = new Button();
            uiButton.AnimationEnabled = true;
            uiButton.Name = name + "helperCompnonent";
            uiButton.SetSize(width, height);
            uiButton.SetPosition(xPos, yPos);
            uiButton.Texture = ResourceCache.GetTexture2D("Textures/" + name + ".png");

            if (action != null)
                uiButton.Pressed += action;

            uiRoot.AddChild(uiButton);

            return uiButton;
        }

        Node CreateNodeButton(Node parentNode, string name, float Xpos, float Ypos, float scale)
        {
            Node nodeButton = parentNode.CreateChild(name);
            nodeButton.SetScale(scale);
            nodeButton.Position = new Vector3(Xpos, Ypos, 0);
            nodeButton.CreateComponent<StaticSprite2D>();
            nodeButton.GetComponent<StaticSprite2D>().Sprite = ResourceCache.GetSprite2D("Images/" + name + ".png");
            return nodeButton;
        }

        void Plan_Pressed(PressedEventArgs args)
        {
            CreatePickerWindow();
        }

        private void Clone_Pressed(PressedEventArgs obj)
        {
            if (pickedNode != null)
            {
                Node clonedNode = pickedNode.Clone(CreateMode.Replicated);
                clonedNode.RunActionsAsync(new TintTo(1f, 1f, 1f, 1f));
                clonedNode.Position = new Vector3(pickedNode.Position.X + 2.5f, pickedNode.Position.Y, 0);
                scene.AddChild(clonedNode);
            }
        }

        private void Minus_Pressed(PressedEventArgs obj)
        {
            ZoomOut = true;
        }

        private void Plus_Pressed(PressedEventArgs obj)
        {
            ZoomIn = true;
        }

        private void Rotate_Pressed(PressedEventArgs obj)
        {
            Rotate = true;
        }
    }
}
