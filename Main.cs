using BananaOS;
using BananaOS.Pages;
using BepInEx;
using System;
using System.Collections.Generic;
using System.Text;
using Utilla;

namespace BananaOSLongarms
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Main : BaseUnityPlugin
    {
        public static bool inRoom = false;
        void Start()
        {
            MonkeWatch.RegisterPage(typeof(LongArmPage));
        }
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        public void OnGameInitialized()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
            inRoom = false;
        }
    }
    }
