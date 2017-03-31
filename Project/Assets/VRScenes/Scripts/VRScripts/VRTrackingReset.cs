using UnityEngine;
using UnityEngine.VR;

namespace VRStandardAssets.Utils
{
   /*
    * This class simply insures the head tracking behaves correctly when the application is paused.
    * this is to stop any nausea that the player may feel if the game is paused
    */
    public class VRTrackingReset : MonoBehaviour
    {
        // when the game is paused
        private void OnApplicationPause(bool pauseStatus)
        {
            InputTracking.Recenter();
        }
    }
}