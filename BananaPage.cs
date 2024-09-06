using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using BananaOS;
using BananaOS.Pages;

namespace GorillaTrails
{
    public  class BananaPage : WatchPage
    {
        public override string Title => "Gorilla Trails";

        public override bool DisplayOnMainMenu => true;

        public override void OnPostModSetup()
        {
            selectionHandler.maxIndex = 1;
        }

        public override string OnGetScreenContent()
        {
            StringBuilder sr = new StringBuilder();
            sr.AppendLine("<align=center>Gorilla Trails Page</align>");
            sr.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, $"Trail Time: {TrailMod.Instance.trail.time}"));
            sr.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, $"Use Color: {TrailMod.Instance.useColor}"));
            return sr.ToString();
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;
                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;

                case WatchButtonType.Right:
                    if (selectionHandler.currentIndex == 0)
                    {
                        TrailMod.Instance.trail.time += 0.1f;
                    }
                    break;
                case WatchButtonType.Left:
                    if (selectionHandler.currentIndex == 0)
                    {
                        TrailMod.Instance.trail.time -= 0.1f;
                    }
                    break;
                case WatchButtonType.Enter:
                    if (selectionHandler.currentIndex == 1)
                    {
                        TrailMod.Instance.useColor = !TrailMod.Instance.useColor;
                    }
                    break;

            }
        }
    }
}
