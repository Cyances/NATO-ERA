﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHPC.Camera;
using GHPC.Player;
using MelonLoader;
using UnityEngine;
using GHPC.State;
using System.Collections;
using GHPC.Weapons;
using GHPC.Equipment.Optics;
using GHPC.Vehicle;
using System.Reflection;
using Reticle;
using GHPC.Equipment;
using GHPC.Utility;
using GHPC;
using HarmonyLib;

namespace NatoEra
{
    public static class M2Bradley
    {

        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> showERAm2;
        static MelonPreferences_Entry<bool> showERAm2_HullSideExtended;
        static MelonPreferences_Entry<bool> showERAm2_HullSideLower;
        static MelonPreferences_Entry<bool> showERAm2_HullSideLowerExtended;
        static MelonPreferences_Entry<bool> showERAm2_HullLowerFront;
        static MelonPreferences_Entry<bool> showERAm2_HullUpper;

        static MelonPreferences_Entry<bool> featherBRAT;

        public static void Config(MelonPreferences_Category cfg)
        {
            showERAm2 = cfg.CreateEntry<bool>("M2 ERA", true);
            showERAm2.Description = "Enable ERA for M2";

            showERAm2_HullSideExtended = cfg.CreateEntry<bool>("M2 Extended Hull Side ERA", false);
            showERAm2_HullSideExtended.Description = "M2 Hull ERA Arrangement";
            showERAm2_HullSideLower = cfg.CreateEntry<bool>("M2 Hull Side Lower ERA", true);
            showERAm2_HullSideLowerExtended = cfg.CreateEntry<bool>("M2 Extended Hull Side Lower ERA", false);
            showERAm2_HullLowerFront = cfg.CreateEntry<bool>("M2 Hull Lower Front ERA", false);
            showERAm2_HullUpper = cfg.CreateEntry<bool>("M2 Hull Upper ERA", false);

            featherBRAT = cfg.CreateEntry<bool>("Feather BRAT", true);
            featherBRAT.Description = "Removes BRAT weight penalty";
        }

        public static IEnumerator Convert(GameState _)
        {
            ////Apply BRAT
            if (showERAm2.Value)
            {
                foreach (GameObject vic_go in NatoERA.vic_gos)
                {
                    Vehicle vic = vic_go.GetComponent<Vehicle>();

                    if (vic == null) continue;

                    if (vic_go.GetComponent<Util.AlreadyConvertedNERA>() != null) continue;
                    if (vic_go.GetComponent<Util.HasBRAT>() != null) continue;
                    if (vic.FriendlyName == "M2 Bradley")
                    {
                        vic_go.AddComponent<Util.AlreadyConvertedNERA>();
                        vic_go.AddComponent<Util.HasBRAT>();

                        var hull_late_followers = vic.GetComponent<LateFollowTarget>()._lateFollowers;
                        GameObject m2_hull_array = GameObject.Instantiate(BRAT.BRAT_m2_hull_array, hull_late_followers.Where(o => o.name == "BRADLEY HULL FOLLOW").First().transform.Find("HULL"));

                        m2_hull_array.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        m2_hull_array.transform.localPosition = new Vector3(0f, 0.269f, 0.112f);

                        if (!showERAm2_HullSideLower.Value)
                        {
                            GameObject.Destroy(m2_hull_array.transform.Find("Side Lower Array").gameObject);
                        }

                        if (!showERAm2_HullSideLowerExtended.Value)
                        {
                            GameObject.Destroy(m2_hull_array.transform.Find("Side Lower Extended Array").gameObject);
                        }

                        if (!showERAm2_HullSideExtended.Value)
                        {
                            GameObject.Destroy(m2_hull_array.transform.Find("Side Extended Array").gameObject);
                        }

                        if (!showERAm2_HullLowerFront.Value)
                        {
                            GameObject.Destroy(m2_hull_array.transform.Find("Front Lower Array").gameObject);
                        }

                        if (!showERAm2_HullUpper.Value)
                        {
                            GameObject.Destroy(m2_hull_array.transform.Find("Hull Upper Array").gameObject);
                        }

                        Rigidbody m2Rb = vic_go.GetComponent<Rigidbody>();
                        int mass_m2_Hull = featherBRAT.Value ? 0 : 2211;
                        int mass_m2_HullSideLower = featherBRAT.Value ? 0 : 825;
                        int mass_m2_HullSideLowerExtended = featherBRAT.Value ? 0 : 118;
                        int mass_m2_HullSideExtended = featherBRAT.Value ? 0 : 531;
                        int mass_m2_FrontLower = featherBRAT.Value ? 0 : 251;
                        int mass_m2_HullUpper = featherBRAT.Value ? 0 : 671;

                        m2Rb.mass += mass_m2_Hull;

                        if (showERAm2_HullSideExtended.Value)
                        {
                            m2Rb.mass += mass_m2_HullSideExtended;
                        }

                        if (showERAm2_HullSideLower.Value)
                        {
                            m2Rb.mass += mass_m2_HullSideLower;
                        }

                        if (showERAm2_HullSideLowerExtended.Value)
                        {
                            m2Rb.mass += mass_m2_HullSideLowerExtended;
                        }

                        if (showERAm2_HullLowerFront.Value)
                        {
                            m2Rb.mass += mass_m2_FrontLower;
                        }

                        if (showERAm2_HullUpper.Value)
                        {
                            m2Rb.mass += mass_m2_HullUpper;
                        }
                    }
                }
            }
            yield break;
        }
        public static void Init()
        {
            StateController.RunOrDefer(GameState.GameReady, new GameStateEventHandler(Convert), GameStatePriority.Lowest);
        }
    }
}
