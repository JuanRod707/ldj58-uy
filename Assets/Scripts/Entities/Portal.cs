using Assets.Scripts.Director;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Portal : MonoBehaviour
    {
        Progress progress;

        public void Initialize(Progress progress) => 
            this.progress = progress;

        public void OnSoulDelivered() => 
            progress.DeliverSoul();
    }
}
