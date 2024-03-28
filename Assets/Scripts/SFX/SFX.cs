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
}
