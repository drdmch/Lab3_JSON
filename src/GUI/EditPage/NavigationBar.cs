using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace GUI;

public class NavigationBar : HorizontalStackLayout
{
    public Button BackButton { get; }
    public Label TitleLabel { get; }
    public Button RightButton { get; }

    public NavigationBar()
    {
        HorizontalOptions = LayoutOptions.FillAndExpand;

        BackButton = new Button()
        {
            Text = "Back",
            Command = new Command(() =>
            {
                // Handle back button press
            })
        };

        TitleLabel = new Label()
        {
            Text = "Title",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        RightButton = new Button()
        {
            Text = "",
            Command = new Command(() =>
            {
                // Handle button press
            })
        };

        Children.Add(BackButton);
        Children.Add(TitleLabel);
        Children.Add(RightButton);
    }
}