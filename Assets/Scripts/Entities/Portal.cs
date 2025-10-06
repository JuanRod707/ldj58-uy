using Assets.Scripts.Common;
using Assets.Scripts.Director;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] AudioHelper deliverSfx;

        Progress progress;

        public void Initialize(Progress progress) => 
            this.progress = progress;
        
        public void OnDeliver()
        {
            deliverSfx.PlayRandomOneShot();
            progress.DeliverSoul();
        }
    }
}
