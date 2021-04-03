using System;
using Seal.CustomUI;
using Seal.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomDropDown), typeof(DropdownRenderer))]
namespace Seal.iOS
{
    public class DropdownRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            var element = (CustomDropDown)this.Element;

            if (this.Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
            {
                var downarrow = UIImage.FromBundle(element.Image);
                Control.RightViewMode = UITextFieldViewMode.Always;
                Control.RightView = new UIImageView(downarrow);
            }
        }
    }
}
