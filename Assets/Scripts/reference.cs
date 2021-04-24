// //Donut Ass------------------
// public class DonutAss : MonoBehaviour
// {
//     public float Fill
//     {
//         get
//         {
//             return curT_;
//         }
//         set 
//         {
//             curT_ = value;
//             progressBarUI_.fillAmount = curT_;
//         }
//     }
    
//     private float curT_;

//     public Image progressBarUI_;
// }
// ////////////////////////////

// //Scene Management--------
// using UnityEngine.SceneManagement;
// SceneManager.sceneLoaded += OnSceneLoaded;
// private void OnSceneLoaded (Scene scene, LoadSceneMode mode)
// {
//     return scene.name;
// }
// SceneManager.LoadScene(sceneName);
// //////////////////////////////

// //Remap-------------------
// public static float Remap(this float value, float fromStart, float fromEnd, float toStart, float toEnd)
// {
//     float t = (value - fromStart) / (fromEnd - fromStart);
//     return t * (toEnd - toStart) + toStart;
// }
// ///////////

// //Subscribe to player input----------------
// playerInput.Radio.NextStation.performed += _ => ChangeRadio(true);
// /////////////////

// //Event Structure-----------------
// public delegate void Overheat();
// public event Overheat OnOverheat;
// public void Overheated()
// {
//     OnOverheat?.Invoke();
// }
// OnOverheat += _ => MyMethod();
// ////////////////

// //Singleton pattern---------------
// public static class Singleton
// {
//     public static float Value()
//     {
//         return 1f;
//     }
// }
// float x = 25 + Singleton.Value();
// ////////////////

// //FMOD Stuff--------
// using UnityEngine.Audio;

// [FMODUnity.EventRef]
// public string event;
     
// FMOD.Studio.EventInstance timeline;
// public FMODUnity.StudioEventEmitter emitter;

// FMODUnity.RuntimeManager.PlayOneShot(event);

// emitter.Stop();emitter.Start();
// emitter.SetParameter("Parameter", value);

// timeline = FMODUnity.RuntimeManager.CreateInstance(event);
// timeline.start();
// timeline.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

// FMODUnity.RuntimeManager
//   .StudioSystem.setParameterByName("EQ Global", eq);
// ////////////////