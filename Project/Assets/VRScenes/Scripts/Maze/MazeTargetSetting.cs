using System;
using UnityEngine;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Maze
{
   /*
    * This is a simple class used in the maze scene
    * that determines when the character can be given
    * new target destinations.
    */
    public class MazeTargetSetting : MonoBehaviour
    {
        public event Action<Transform> OnTargetSet;                     // This is triggered when a destination is set.

        [SerializeField] private Reticle m_Reticle;                     // This is used to reference the position and use it as the destination.
        [SerializeField] private VRInteractiveItem m_InteractiveItem;   // The VRInteractiveItem on the maze, used to detect double clicks on the maze.

        private bool m_Active;                                          // This determines whether the character can be given targets or not.

        // handle the double click for the marker
        private void OnEnable()
        {
            m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
        }

        // stop handling
        private void OnDisable()
        {
            m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
        }

        // if the marker is active the set it to true
        public void Activate ()
        {
            m_Active = true;
        }

        // when it is not being used the false
        public void Deactivate ()
        {
            m_Active = false;
        }

        private void HandleDoubleClick()
        {
            // If target setting is active and there are subscribers to OnTargetSet, call it.
            if (m_Active && OnTargetSet != null)
                    OnTargetSet (m_Reticle.ReticleTransform);
        }
    }
}