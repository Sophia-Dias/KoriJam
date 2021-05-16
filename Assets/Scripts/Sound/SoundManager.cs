using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  public static SoundManager Instance;
  public Texture2D texture;
  public float shaderDuration;

  [SerializeField] private List<SoundController> allSounds;

  public static void AddSound(SoundController soundController) {
    if (Instance.allSounds.Contains(soundController)) return;
    Instance.allSounds.Add(soundController);
  }

  public static void RemoveSound (SoundController soundController) {
    if (!Instance.allSounds.Contains(soundController)) return;
    Instance.allSounds.Remove(soundController);
  }

  private void Awake() {
    if(Instance == null) Instance = this;
    if(Instance != this) Destroy(this);
  }

  private void Start() {
    if(allSounds== null) allSounds = new List<SoundController>();

    for (var positionIndex = 0; positionIndex < 20; positionIndex++)
    {
      Shader.SetGlobalVector($"_Position{positionIndex}", Vector4.zero);
    }
  }

  private void Update() {
    for (var soundIndex = 0; soundIndex < 20; soundIndex++)
    {
      if(soundIndex >= allSounds.Count) {
        Shader.SetGlobalVector($"_Position{soundIndex}", Vector4.zero);
        break;
      }

      SoundController sound = allSounds[soundIndex];
      Vector4 pos = sound.transform.position;
      pos.w = sound.range;
      Shader.SetGlobalVector($"_Position{soundIndex}", pos);
      Shader.SetGlobalFloat($"_ScriptTime{soundIndex}", sound.time);
    }
  }
}
