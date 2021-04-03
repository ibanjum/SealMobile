using System;
using System.Collections.Generic;
using Seal.Models;
using Urho;
using Urho.Gui;
using Urho.Resources;

namespace Seal.GraphicalInterface.UpdateEdit
{
    public partial class FloorPlanGUI
    {
        UIElement catagoriesContainer;
        UIElement scrollViewContainer;
        UIElement pickerContainer;
        ScrollView scrollView;

        void CreatePickerWindow()
        {
            pickerContainer = new Window();
            pickerContainer.SetColor(Color.Gray);
            uiRoot.AddChild(pickerContainer);
            pickerContainer.SetMinSize(Graphics.Width, Graphics.Height);
            pickerContainer.LayoutMode = LayoutMode.Vertical;

            AddSearchBar();

            Button seperator = new Button();
            seperator.SetFixedSize(Graphics.Width, 1);
            seperator.SetColor(Color.White);
            pickerContainer.AddChild(seperator);

            catagoriesContainer = new UIElement();
            catagoriesContainer.SetFixedSize(Graphics.Width, 140);
            catagoriesContainer.LayoutSpacing = 20;
            catagoriesContainer.SetPosition(0, Graphics.Height - 140);
            catagoriesContainer.LayoutMode = LayoutMode.Horizontal;
            catagoriesContainer.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            pickerContainer.AddChild(catagoriesContainer);


            scrollView = new ScrollView();
            scrollView.SetDeepEnabled(true);
            scrollView.SetFixedSize(Graphics.Width, Graphics.Height - 240);
            scrollView.SetPosition(0, Graphics.Height - 240);
            scrollView.Enabled = false;
            scrollView.AutoDisableChildren = true;
            pickerContainer.AddChild(scrollView);

            scrollViewContainer = new UIElement();
            scrollViewContainer.LayoutMode = LayoutMode.Horizontal;
            scrollView.ContentElement = scrollViewContainer;

            int i = 0;
            foreach (var category in Categories)
            {
                AddCategory(category.Name, i);
                i++;
                CreateCollectionView(category.CategoryItems);
            }

        }

        void AddSearchBar()
        {
            UIElement searhBar = new UIElement();
            searhBar.SetFixedSize(Graphics.Width, 140);
            searhBar.LayoutSpacing = 20;
            searhBar.SetPosition(0, Graphics.Height - 140);
            searhBar.LayoutMode = LayoutMode.Horizontal;
            pickerContainer.AddChild(searhBar);

            Button backButton = new Button();
            backButton.SetFixedSize(40, 40);
            backButton.SetColor(Color.White);
            backButton.BlendMode = BlendMode.InvDestAlpha;
            backButton.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            //backButton.Texture = ResourceCache.GetTexture2D("Textures/back.png");
            backButton.Pressed += BackButton_Pressed;
            searhBar.AddChild(backButton);

            Button searhIcon = new Button();
            searhIcon.SetFixedSize(40, 40);
            searhIcon.SetColor(Color.White);
            searhIcon.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            searhIcon.Texture = ResourceCache.GetTexture2D("Textures/search.png");
            searhBar.AddChild(searhIcon);

            Font font = ResourceCache.GetFont("Fonts/Anonymous Pro.ttf");

            LineEdit searchEdit = new LineEdit();
            searchEdit.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            searchEdit.SetColor(Color.Gray);
            searchEdit.SetFixedHeight(60);
            searchEdit.CursorBlinkRate = 10000f;
            searchEdit.Cursor.VerticalAlignment = VerticalAlignment.Center;
            searchEdit.Cursor.SetFixedWidth(3);
            searchEdit.TextChanged += OnSearchBarTextChanged;
            searchEdit.TextElement.SetColor(Color.White);
            searchEdit.TextElement.Value = "Search";
            searchEdit.TextElement.TextEffect = TextEffect.Stroke;
            searchEdit.TextElement.EffectColor = new Color(1, 1f, 1f);
            searchEdit.TextElement.SetAlignment(HorizontalAlignment.Left, VerticalAlignment.Center);
            searchEdit.TextElement.SetFont(font, 35);
            searhBar.AddChild(searchEdit);
        }

        void AddCategory(string name, int i)
        {

            Button categoryButton = new Button();
            categoryButton.Name = i.ToString();
            categoryButton.SetColor(Color.Transparent);
            categoryButton.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            categoryButton.Pressed += Category_Pressed;
            catagoriesContainer.AddChild(categoryButton);

            UIElement sContainer = new UIElement();
            sContainer.LayoutSpacing = 30;
            categoryButton.AddChild(sContainer);
            sContainer.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            sContainer.SetSize(40, 60);

            Text text = new Text();
            text.Value = name;
            text.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            text.SetColor(Color.White);
            text.LayoutMode = LayoutMode.Vertical;
            text.SetFont(font, 35);
            sContainer.AddChild(text);

            if (name == "Tables")
            {
                Button underLine = new Button();
                underLine.Name = "underLine";
                underLine.SetFixedSize(text.Width, 5);
                underLine.SetColor(Color.White);
                underLine.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);
                sContainer.AddChild(underLine);
            }
        }

        void CreateCollectionView(List<ItemModel> items)
        {
            var window = new Window();
            scrollViewContainer.AddChild(window);
            window.SetColor(Color.FromHex("#a6a6a6"));
            window.LayoutMode = LayoutMode.Vertical;


            CreateLayers(items, window);
        }

        void CreateLayers(List<ItemModel> items, Window window)
        {
            UIElement layerContainer = new UIElement();

            for (int i = 0; i < items.Count; i++)
            {

                if (i % 3 == 0)
                {
                    var newContainer = new UIElement();
                    window.AddChild(newContainer);
                    newContainer.SetFixedSize(Graphics.Width, Graphics.Width / 3);
                    newContainer.HorizontalAlignment = HorizontalAlignment.Center;
                    newContainer.LayoutMode = LayoutMode.Horizontal;
                    layerContainer = newContainer;
                }

                var marginContainer = new UIElement();
                layerContainer.AddChild(marginContainer);
                marginContainer.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
                marginContainer.SetFixedSize(Graphics.Width / 3, Graphics.Width / 3);
                marginContainer.LayoutMode = LayoutMode.Horizontal;

                Button itemButton = new Button();
                itemButton.SetFixedSize((Graphics.Width / 3) - 40, (Graphics.Width / 3) - 40);
                itemButton.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
                itemButton.Texture = ResourceCache.GetTexture2D("Textures/" + items[i].ImageName + ".png");
                itemButton.Name = items[i].Name;
                itemButton.Pressed += Item_Pressed;
                marginContainer.AddChild(itemButton);
            }
        }

        private void Category_Pressed(PressedEventArgs obj)
        {
            var button = obj.Element as Button;
            var container = button.Parent;

            foreach (var item in container.Children)
            {
                var scontainer = item.Children[0];
                scontainer.RemoveChildAtIndex(1);
            }

            var selectedContainer = button.Children[0];
            Button underLine = new Button();
            underLine.Name = "underLine";
            underLine.SetFixedSize(selectedContainer.Children[0].Width, 5);
            underLine.SetColor(Color.White);
            underLine.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);
            selectedContainer.AddChild(underLine);

            scrollView.SetViewPosition(Graphics.Width * Convert.ToInt32(button.Name), 0);
        }

        private void Item_Pressed(PressedEventArgs obj)
        {
            UIElement selectedItem = obj.Element;
            CreateItem(selectedItem);
            pickerContainer.Remove();
        }

        private void OnSearchBarTextChanged(TextChangedEventArgs obj)
        {
            LineEdit searchEdit = obj.Element as LineEdit;
            if (string.IsNullOrEmpty(searchEdit.Text))
            {
                searchEdit.TextElement.Value = "Search";
            }
        }

        private void BackButton_Pressed(PressedEventArgs obj)
        {
            pickerContainer.Remove();
        }

        void ItemPressed(PressedEventArgs args)
        {
            UIElement item = args.Element;
            if (item != null)
            {
                CreateItem(item);
            }
        }
    }
}
