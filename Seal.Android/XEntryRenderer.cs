using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Seal.CustomUI;
using Android.Content;
using Seal.Droid;

[assembly: ExportRenderer(typeof(CustomEntryRenderer), typeof(MyEntryRenderer))]
namespace Seal.Droid
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            }
        }
    }
}