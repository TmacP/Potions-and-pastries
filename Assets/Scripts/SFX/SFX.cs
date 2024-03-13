using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public FMOD.Studio.EventInstance sfx;
    private void PlayFootstep()
    {
        sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Footstep");
        sfx.start();
        sfx.release();
    }
}
