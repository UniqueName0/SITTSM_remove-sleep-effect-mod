using BepInEx;
using HarmonyLib;
using UnityEngine;
using System;
using System.Reflection;
using HarmonyLib.Tools;
namespace remove_sleep
{
    [BepInProcess("Stick It To The (Stick) Man.exe")]
    [BepInPlugin("uniquename.sittsm.remove-sleep", "remove-sleep", "0.0.0.0")]
    public class remove_sleep_effect : BaseUnityPlugin
    {

        public void Awake()
        {
            var harmony = new Harmony("uniquename.sittsm.remove-sleep");
            harmony.PatchAll();
        }

        public const string modID = "uniquename.sittsm.remove-sleep";
        public const string modName = "remove-sleep";
    }

    [HarmonyPatch(typeof(MovementStateCondition), "Evaluate")]
    public class remove_sleep_Patch
    {
        public static bool Prefix(CharacterMovement character, MovementStateCondition __instance, ref bool __result)
        {
            __result = (__instance._isInAir && character.IsInAir(1.1f, 0f)) || (__instance._isOnGround && !character.IsInAir(1f, 0f)) || (__instance._isNotOnFire && !character.Health.CharacterOnFire.IsBurning) || (__instance._isNotTumbling && !character.IsInAir(1f, 0f) && Mathf.Abs(character.Torso.velocity.y) < 2f && character.Torso.transform.up.y > 0f && Mathf.Abs(character.Torso.velocity.x) < 5f) || (__instance._affectedByToxicOdours && (!SingletonBehaviour<PassiveManager>.HasInstance || !SingletonBehaviour<PassiveManager>.Instance.AreOdoursToxic()));
            return false;
        }
    }
}