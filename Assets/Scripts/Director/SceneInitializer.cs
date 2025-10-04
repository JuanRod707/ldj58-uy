using UnityEngine;

namespace Assets.Scripts.Director
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] NPCInitializer npcs;

        void Start()
        {
            npcs.Initialize();
        }
    }
}
