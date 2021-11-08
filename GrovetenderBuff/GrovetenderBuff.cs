using BepInEx;
using BepInEx.Configuration;
using R2API.Utils;
using RoR2;
using UnityEngine;

namespace GrovetenderBuff
{
    [BepInPlugin("com.Moffein.GrovetenderBuff", "Grovetender Buff", "1.0.0")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.DifferentModVersionsAreOk)]
    public class GrovetenderBuff : BaseUnityPlugin
    {
        public void Awake()
        {
            GameObject trackingWispObject = Resources.Load<GameObject>("prefabs/projectiles/GravekeeperTrackingFireball");
            HealthComponent hc = trackingWispObject.GetComponent<HealthComponent>();
            hc.globalDeathEventChanceCoefficient = 0f;

            bool noKill = base.Config.Bind<bool>(new ConfigDefinition("Settings", "Disable HurtBoxes"), true,
                new ConfigDescription("Make Grovetender Wisps unkillable.")).Value;
            if (noKill)
            {
                hc.dontShowHealthbar = true;
                trackingWispObject.AddComponent<DisableHurtbox>();
            }
        }
    }

    public class DisableHurtbox : MonoBehaviour
    {
        public void Start()
        {
            this.gameObject.GetComponent<CharacterBody>().hurtBoxGroup.SetHurtboxesActive(false);
        }
    }
}
