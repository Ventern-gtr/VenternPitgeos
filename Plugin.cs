using BepInEx;
using System;
using System.IO;
using UnityEngine;

namespace VenternPitgeos
{
    [BepInPlugin("Ventern.Pitgeos", "Ventern - Pitgeos", "0.1")]
    public class Plugin : BaseUnityPlugin
    {
        internal static Rect windowRect = new Rect(20, 20, 250, 150);
        internal static bool showWindow = true;
        internal static bool toggleValue = false;

        void OnGUI()
        {
            if (showWindow)
            {
                windowRect = GUI.Window(0, windowRect, DrawWindow, "My Custom Window");
            }
        }

        void DrawWindow(int windowID)
        {
            GUILayout.Label("Hello from OnGUI!");
            toggleValue = GUILayout.Toggle(toggleValue, "Enable Something");

            if (GUILayout.Button("Close Window"))
            {
                showWindow = false;
            }

            // Allow dragging the window
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }
    }
}
