using BepInEx;
using GorillaNetworking;
using System;
using UnityEngine;
using Utilla;

namespace GorillaTrails
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class TrailMod : BaseUnityPlugin
    {
        public static TrailMod Instance;
        public bool useColor = false;
        bool inRoom;
        bool init;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
            Instance = this;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        public TrailRenderer trail = null;
        void OnGameInitialized(object sender, EventArgs e)
        {
            init = true;
            trail = GorillaTagger.Instance.offlineVRRig.gameObject.AddComponent<TrailRenderer>();
            trail.endWidth = 0.1f;
            trail.startWidth = 0.1f;
            trail.time = 0.2f;
            //trail.material = GorillaTagger.Instance.offlineVRRig.mainSkin.sharedMaterials[0];
        }



        void Update()
        {
            if (init) {
                
                if (useColor)
                {
                    Shader shader = GorillaTagger.Instance.offlineVRRig.mainSkin.material.shader;
                    Color playerColor = GorillaTagger.Instance.offlineVRRig.mainSkin.material.color;
                    Material material = new Material(shader);
                    material.color = playerColor;
                    trail.material = material;
                    
                }
                else
                {
                    trail.material = GorillaTagger.Instance.offlineVRRig.mainSkin.sharedMaterials[0];
                }
                
            }
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = false;
        }
    }
}
