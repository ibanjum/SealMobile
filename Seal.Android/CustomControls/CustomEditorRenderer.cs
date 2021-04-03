using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Seal.CustomUI;
using Android.Content;
using Seal.Droid.CustomControls;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace Seal.Droid.CustomControls
{
    public class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            }
        }
    }
}
