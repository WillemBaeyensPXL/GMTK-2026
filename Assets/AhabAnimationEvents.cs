using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhabAnimationEvents : MonoBehaviour
{
    [SerializeField]
    private float volume = 1f;

    public void FootStep()
    {
        AudioClip footstepClip = SoundManager.Instance.SoundEffects.PlayerFootsteps[Random.Range(0, SoundManager.Instance.SoundEffects.PlayerFootsteps.Length)];
        SoundManager.Instance.PlaySFXPitch(footstepClip,volume,Random.Range(1f,3f));
    }
}
