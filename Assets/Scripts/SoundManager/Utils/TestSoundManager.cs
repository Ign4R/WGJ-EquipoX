using UnityEngine;
using System.Collections;
using UnityEditor;

public class TestSoundManager : MonoBehaviour
{
   public string soundName;
   [Range(0, 1)]
   public float volume;
}


[CustomEditor(typeof(TestSoundManager))]
public class OpenFileButtonScript : Editor 
{
    public override void OnInspectorGUI()
    {
        TestSoundManager testSoundManager = (TestSoundManager)target;
        testSoundManager.soundName = EditorGUILayout.TextField("SoundName", testSoundManager.soundName);
        testSoundManager.volume = EditorGUILayout.Slider("Volume",testSoundManager.volume,0,1);
        if(GUILayout.Button("PlaySound"))
        {
            AudioManager.main.Play(testSoundManager.soundName);
        }
        if(GUILayout.Button("StopSound"))
        {
            AudioManager.main.Stop(testSoundManager.soundName);
        }
        if(GUILayout.Button("MuteSound"))
        {
            AudioManager.main.Mute(testSoundManager.soundName);
        }
        if(GUILayout.Button("SetVolume"))
        {
            AudioManager.main.Volume(testSoundManager.soundName, testSoundManager.volume);
        }
    }
}