using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using NatoEra;
using GHPC.State;
using System.Collections;
using UnityEngine;
using GHPC.Camera;
using GHPC.Player;

[assembly: MelonInfo(typeof(NatoERA), "1 NATO ERA", "1.2.1", "Cyance")]
[assembly: MelonGame("Radian Simulations LLC", "GHPC")]

namespace NatoEra
{
    public class NatoERA : MelonMod
    {

        public static GameObject[] vic_gos;
        public static GameObject gameManager;
        public static CameraManager camManager;
        public static PlayerInput playerManager;


        public IEnumerator GetVics(GameState _)
        {
            vic_gos = GameObject.FindGameObjectsWithTag("Vehicle");

            yield break;
        }

        public override void OnInitializeMelon()
        {
            MelonPreferences_Category cfg = MelonPreferences.CreateCategory("NATOERAConfig");
            M1Abrams.Config(cfg);
            ARAT.Config(cfg);
            M2Bradley.Config(cfg);
            BRAT.Config(cfg);
            //M60.Config(cfg);
            //PRAT.Config(cfg);
        }

        public override void OnSceneWasLoaded(int idx, string scene_name)
        {
            if (scene_name == "MainMenu2_Scene" || scene_name == "LOADER_MENU" || scene_name == "LOADER_INITIAL" || scene_name == "t64_menu" || scene_name == "MainMenu2-1_Scene") return;

            gameManager = GameObject.Find("_APP_GHPC_");
            camManager = gameManager.GetComponent<CameraManager>();
            playerManager = gameManager.GetComponent<PlayerInput>();

            StateController.RunOrDefer(GameState.GameReady, new GameStateEventHandler(GetVics), GameStatePriority.Low);
            M1Abrams.Init();
            ARAT.Init();
            M2Bradley.Init();
            BRAT.Init();
            //M60.Init();
            //PRAT.Init();
        }
    }
}
