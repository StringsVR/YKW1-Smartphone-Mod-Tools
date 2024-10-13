using ImGui.Forms;
using ImGui.Forms.Controls;
using ImGui.Forms.Controls.Base;
using ImGui.Forms.Controls.Layouts;
using ImGui.Forms.Controls.Menu;
using ImGui.Forms.Factories;
using ImGui.Forms.Models;
using ImGui.Forms.Resources;
using ImGuiNET;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Size = ImGui.Forms.Models.Size;

namespace YKW1_Smartphone_Mod_Tools
{
    public static class AddUI
    {
        public static MenuBarMenu AddMenuItems(string Title, List<string> items, List<Action> actions)
        {
            if (items.Count != actions.Count)
            {
                throw new ArgumentException("The number of items must match the number of actions.");
            }

            var Menu = new MenuBarMenu(Title);

            for (int i = 0; i < items.Count; i++)
            {
                var menuItem = new MenuBarButton(items[i]);
                // Capture the current index in a local variable to avoid closure issues
                int index = i;
                menuItem.Clicked += (sender, e) => actions[index]();
                Menu.Items.Add(menuItem);
            }
            return Menu;
        }

        public static Label MakeLabel(string text, FontResource font)
        {
            var label = new Label(text)
            {
                Font = font, //Set desired text color
                TextColor = Color.AliceBlue,
            };
            return label;
        }

        public static Button MakeButton(string text, int fontSize)
        {
            var button = new Button(text)
            {
                Font = FontFactory.Get("Roboto", fontSize),
                Padding = new System.Numerics.Vector2(10, 2)
            };
            return button;

        }

        public static StackLayout createTitleLayout(Label label, params Button[] buttons)
        {
            var titleLayout = new StackLayout
            {
                Size = new Size(SizeValue.Parent, SizeValue.Relative(.25f)),
                Items = { label }
            };

            if (buttons.Length > 1)
            {
                titleLayout.Items.Add(createButtonLayout(buttons));
            }
            else
            {
                // Reducing nested layouts for just one button
                titleLayout.Items.Add(new StackItem(buttons[0])
                {
                    Size = Size.Parent,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom
                });
            }

            return titleLayout;
        }

        public static StackLayout createImportTitleLayout(Label label, Label label2, params Button[] buttons)
        {
            var titleLayout = new StackLayout
            {
                Size = new Size(SizeValue.Parent, SizeValue.Relative(.25f)),
                Items = { label, label2 }
            };

            if (buttons.Length > 1)
            {
                titleLayout.Items.Add(createButtonLayout(buttons));
            }
            else
            {
                // Reducing nested layouts for just one button
                titleLayout.Items.Add(new StackItem(buttons[0])
                {
                    Size = Size.Parent,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom
                });
            }

            return titleLayout;
        }

        public static StackLayout createButtonLayout(params Button[] buttons)
        {
            var buttonLayout = new StackLayout
            {
                Alignment = Alignment.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                ItemSpacing = 7
            };

            foreach (var button in buttons)
                buttonLayout.Items.Add(button);

            return buttonLayout;
        }

        public static StackLayout addItemAsStackItem(StackLayout layout, Component item, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, Size size)
        {
            layout.Items.Add(new StackItem(item)
            {
                Size = size,
                HorizontalAlignment = horizontalAlignment,
                VerticalAlignment = verticalAlignment
            });
            return layout;
        }
    }
}
