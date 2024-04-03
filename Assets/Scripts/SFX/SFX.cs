using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static FMOD.Studio.EventInstance sfx;
    private void PlayFootstep()
    {
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Footstep");
        sfx.start();
        sfx.release();
    }

    public static void PlayDoorOpen()
    {
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/doorOpen_1");
        sfx.start();
        sfx.release();
    }

    public static void PlayGateOpen()
    {
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/creak1");
        sfx.start();
        sfx.release();
    }

    public static void PlayFireworks() // gatherable success
    {
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/fireworks");
        sfx.start();
        sfx.release();
    }

    public static void PlayCard() // play card into crafting station
    {
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/card");
        sfx.start();
        sfx.release();
    }
    // CRAFTING STATINO SOUNDS
    // mixer sound
    public static void PlayMixer()
    {
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/mixer");
        sfx.start();
        sfx.release();
    }

    // cauldron sound
    public static void PlayCauldron()
    {
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/cauldron");
        sfx.start();
        sfx.release();
    }
    // fireplace 
    public static void PlayFireplace()
    {
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/fireplace");
        sfx.start();
        sfx.release();
    }
}

// to use just add code like this to your script:
//SFX.PlayDoorOpen();
// SFX is static so dont need to create an instance of it