using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Urho;
using Urho.Actions;
using Urho.Gui;
using Urho.Urho2D;

namespace Seal.GraphicalInterface.UpdateEdit
{
    public partial class FloorPlanGUI
    {
        bool scaling;
        Node EnvironmentNode;

        protected const float TouchSensitivity = 2;
        protected float Yaw { get; set; }
        protected float Pitch { get; set; }

        protected override void OnUpdate(float timeStep)
        {
            base.OnUpdate(timeStep);

            if (pickedNode != null)
            {
                if (ZoomIn)
                {
                    pickedNode.SetScale(pickedNode.Scale.X + 0.02f);
                }
                if (ZoomOut)
                {
                    pickedNode.SetScale(pickedNode.Scale.X - 0.02f);
                }

                if (Input.NumTouches == 1)
                {
                    TouchState state = Input.GetTouch(0);
                    var touchPosition = state.Position;
                    var lastPosition = state.LastPosition;

                    if (!locked)
                    {
                        if (Drag && !Rotate)
                        {
                            pickedNode.GetComponent<RigidBody2D>().SetLinearVelocity(
                                new Vector2(touchPosition.X - lastPosition.X, -(touchPosition.Y - lastPosition.Y)) * 0.5f);

                            HandleDelete();
                        }
                        else if (Rotate && buttonRotate != null)
                        {
                            pickedNode.Rotation = buttonRotate.Rotation;
                        }
                    }
                    else
                    {
                        var velocity = new Vector3(touchPosition.X - lastPosition.X, -(touchPosition.Y - lastPosition.Y), 0) * 0.5f;
                        pickedNode.Position += (direc + velocity) * 0.5f;
                    }
                }
            }
        }

        async void HandleDelete()
        {
            if (trashButton != null && toolContainer != null)
            {
                trashButton.Enabled = true;
                await trashButton.RunActionsAsync(new ScaleTo(0.1f, 0.2f));
                toolContainer.SetDeepEnabled(false);
            }

            if (pickedNode != null)
            {
                if (pickedNode.Position.Y < -8 + pickedNode.Scale.Y)
                {
                    Delete = true;
                    await trashButton.RunActionsAsync(new ScaleTo(0f, 0.25f));
                }
                else
                {
                    Delete = false;
                }
            }
        }
        async void HandleTouchEnd(TouchEndEventArgs args)
        {
            if (pickedNode != null)
            {
                await trashButton.RunActionsAsync(new ScaleTo(0.1f, 0f));
                trashButton.Enabled = false;

                pickedNode.GetComponent<RigidBody2D>().SetLinearVelocity(new Vector2(0, 0));

                if (Delete)
                {
                    await pickedNode.RunActionsAsync(new ScaleBy(0.1f, 0f));

                    toolContainer.SetDeepEnabled(false);
                    pickedNode.Remove();
                    pickedNode = null;
                }
                else
                {
                    toolContainer.SetDeepEnabled(true);
                }
            }
            ZoomIn = false;
            ZoomOut = false;
            Rotate = false;
            Delete = false;
            Drag = false;
            scaling = false;
        }
        void HandleTouchMove(TouchMoveEventArgs args)
        {
            if (pickedNode != null && buttonRotate != null)
            {

                Vector3 touchPos = new Vector3((float)args.X / Graphics.Width, (float)args.Y / Graphics.Height, 0);
                Vector3 touchPosSTWP = CameraNode.GetComponent<Camera>().ScreenToWorldPoint(touchPos);

                if (Rotate)
                {
                    Vector3 direction = touchPosSTWP - buttonRotate.Position;
                    float angle = MathHelper.RadiansToDegrees((float)Math.Atan2(direction.Y, direction.X));
                    angle = (float)Math.Ceiling(angle / 10) * 5;
                    buttonRotate.Rotation = Quaternion.FromAxisAngle((new Vector3(Vector3.Forward.X, Vector3.Forward.Y, 1)), angle);
                }
                if (Input.NumTouches == 2)
                {
                    scaling = true;
                    var state1 = Input.GetTouch(0);
                    var state2 = Input.GetTouch(1);

                    var distance1 = IntVector2.Distance(state1.Position, state2.Position);
                    var distance2 = IntVector2.Distance(state1.LastPosition, state2.LastPosition);

                    var v1 = new Vector3(state1.Position.X, state1.Position.Y, 0);
                    Vector3 t1 = CameraNode.GetComponent<Camera>().ScreenToWorldPoint(v1);
                    // var v2 = new Vector3(state1.LastPosition.Y, state2.LastPosition.Y, 0);

                    if (pickedNode != null)
                    {
                        var d1 = t1 - pickedNode.Position;
                        pickedNode.Scale = new Vector3(pickedNode.Scale.X + (distance1 - distance2) / 1000f, pickedNode.Scale.X + (distance1 - distance2) / 1000f, 0);
                        float angle = MathHelper.RadiansToDegrees((float)Math.Atan2(d1.Y, d1.X));
                        angle = (float)Math.Ceiling(angle / 10) * 5;
                        pickedNode.Rotation = Quaternion.FromAxisAngle((new Vector3(Vector3.Forward.X, Vector3.Forward.Y, 1)), angle);
                    }
                    else
                    {
                        if (EnvironmentNode.Children.Any())
                        {
                            var d1 = v1 - EnvironmentNode.Position;
                            EnvironmentNode.Scale = new Vector3(EnvironmentNode.Scale.X + (distance1 - distance2) / 1000f, EnvironmentNode.Scale.X + (distance1 - distance2) / 1000f, 0);
                            float angle = MathHelper.RadiansToDegrees((float)Math.Atan2(d1.Y, d1.X));
                            angle = (float)Math.Ceiling(angle / 10) * 5;
                            EnvironmentNode.Rotation = Quaternion.FromAxisAngle((new Vector3(Vector3.Forward.X, Vector3.Forward.Y, 1)), angle);
                        }
                    }
                }
            }
        }

        void HandleTouchBegin(TouchBeginEventArgs args)
        {
            Ray cameraRay = CameraNode.GetComponent<Camera>().GetScreenRay((float)args.X / Graphics.Width, (float)args.Y / Graphics.Height);
            var result = scene.GetComponent<Octree>().RaycastSingle(cameraRay, RayQueryLevel.Triangle, 1000, DrawableFlags.Geometry, uint.MaxValue);

            if (result != null)
            {
                switch (result.Value.Node.Name)
                {
                    case "setting":
                        Rotate = true;
                        break;
                    case "plus":
                        ZoomIn = true;
                        break;
                    case "minus":
                        ZoomOut = true;
                        break;
                    default:
                        if (pickedNode != null && pickedNode != result.Value.Node)
                        {
                            pickedNode.RunActionsAsync(new TintTo(0.2f, 1f, 1f, 1f));
                            pickedNode = null;
                        }

                        Drag = true;
                        pickedNode = result.Value.Node;
                        pickedNode.RunActionsAsync(new TintTo(0.2f, 0.0f, 1.0f, 0.8f));
                        break;
                }
            }
            else
            {
                if (pickedNode != null)
                {
                    pickedNode.RunActionsAsync(new TintTo(0.2f, 1f, 1f, 1f));
                    pickedNode = null;
                }
            }
            Input.TouchMove += HandleTouchMove;
            Input.TouchEnd += HandleTouchEnd;
        }
    }
}
