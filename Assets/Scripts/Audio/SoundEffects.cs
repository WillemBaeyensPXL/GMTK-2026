using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffects", menuName = "Sound/SoundEffects")]
public class SoundEffects : ScriptableObject
{
    [Header("Player")]
    public AudioClip[] PlayerFootsteps;

    [Header("Crew")]
    public AudioClip CrewCollected;
}