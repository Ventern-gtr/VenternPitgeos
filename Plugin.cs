using BepInEx;
using UnityEngine;

namespace VenternPitgeos
{
    [BepInPlugin("Ventern.Pitgeos", "Ventern - Pitgeos", "0.1")]
    public class Plugin : BaseUnityPlugin
    {
        internal static Rect windowRect = new Rect(20, 20, 220, 250);
        internal static bool showWindow = true;
        internal static bool PitGeosEnabled;

        internal GorillaSurfaceOverride pitground;
        internal static float pitgroundValue = 1f;
        internal GorillaSurfaceOverride pitregularwallV2;
        internal static float pitregularwallV2Value = 1f;
        internal GorillaSurfaceOverride pitlowerslipperywall;
        internal static float pitlowerslipperywallValue = 1f;
        internal GorillaSurfaceOverride verticalwall1;
        internal static float verticalwall1Value = 1f;
        internal GorillaSurfaceOverride verticalwall2;
        internal static float verticalwall2Value = 1f;

        void Start()
        {
            pitground = GameObject.Find("pit ground")?.GetComponent<GorillaSurfaceOverride>();
            pitregularwallV2 = GameObject.Find("pitregularwallV2")?.GetComponent<GorillaSurfaceOverride>();
            pitlowerslipperywall = GameObject.Find("pit lower slippery wall")?.GetComponent<GorillaSurfaceOverride>();
            verticalwall1 = GameObject.Find("wallclimb/verticalwall (1)")?.GetComponent<GorillaSurfaceOverride>();
            verticalwall2 = GameObject.Find("wallclimb/verticalwall")?.GetComponent<GorillaSurfaceOverride>();
            VenternPitgeos.Config.LoadConfig();
            VenternPitgeos.Config.SaveConfig();
        }

        void OnGUI()
        {
            if (showWindow)
            {
                GUI.color = Color.red;
                GUI.contentColor = Color.red;
                GUI.backgroundColor = Color.black;
                windowRect = GUI.Window(0, windowRect, DrawWindow, "Ventern Pitgeo's");
            }
        }

        void DrawWindow(int windowID)
        {
            GUI.color = Color.red;
            GUI.contentColor = Color.red;
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button($"Reset to Default"))
            {
                pitgroundValue = 1f;
                pitregularwallV2Value = 1f;
                pitlowerslipperywallValue = 1f;
                verticalwall1Value = 1f;
                verticalwall2Value = 1f;
                ApplyMultiplier(pitground, 1f);
                ApplyMultiplier(pitregularwallV2, 1f);
                ApplyMultiplier(pitlowerslipperywall, 1f);
                ApplyMultiplier(verticalwall1, 1f);
                ApplyMultiplier(verticalwall2, 1f);
            }
            pitgroundValue = Slider("Ground", pitgroundValue);
            pitregularwallV2Value = Slider("Regular Wall ", pitregularwallV2Value);
            pitlowerslipperywallValue = Slider("Slippery Wall", pitlowerslipperywallValue);
            verticalwall1Value = Slider("Double Wall1", verticalwall1Value);
            verticalwall2Value = Slider("Double Wall2", verticalwall2Value);
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }

        void Update()
        {
            if (UnityInput.Current.GetKeyDown(KeyCode.F1))
            {
                showWindow = !showWindow;
            }
            ApplyMultiplier(pitground, pitgroundValue);
            ApplyMultiplier(pitregularwallV2, pitregularwallV2Value);
            ApplyMultiplier(pitlowerslipperywall, pitlowerslipperywallValue);
            ApplyMultiplier(verticalwall1, verticalwall1Value);
            ApplyMultiplier(verticalwall2, verticalwall2Value);
        }

        void ApplyMultiplier(GorillaSurfaceOverride surface, float value)
        {
            if (surface == null) return;
            if (surface.extraVelMultiplier != value)
                surface.extraVelMultiplier = value;
            VenternPitgeos.Config.SaveConfig();
            if (surface.extraVelMaxMultiplier != value)
                surface.extraVelMaxMultiplier = value;
            VenternPitgeos.Config.SaveConfig();
        }

        float Slider(string label, float value)
        {
            GUILayout.Label($"{label}: {value:F1}");
            value = GUILayout.HorizontalSlider(value, 0f, 5f, GUILayout.Width(200));
            value = Mathf.Round(value * 10f) / 10f;
            return value;
        }
    }
}
