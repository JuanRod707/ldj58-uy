using Assets.Scripts.Director;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Compass : MonoBehaviour
    {
        [SerializeField] Transform mainCharacter;
        [SerializeField] RectTransform bounds;
        [SerializeField] Transform pivotTransform;
        [SerializeField] Transform iconTransform;
        [SerializeField] CanvasGroup transparency;
        [SerializeField] SoulProvider soulProvider;
        [SerializeField] float transparencyFactor;

        Transform trackedItem;
        float minX;
        float minY;
        float maxX;
        float maxY;
        
        void Start()
        {
            minX = bounds.sizeDelta.x/2;
            minY = bounds.sizeDelta.y/2;
            maxX = Screen.width - minX;
            maxY = Screen.height - minY;
        }

        void Update()
        {
            trackedItem = soulProvider.Any() ? soulProvider.GetClosestTo(mainCharacter.position).transform : null;
            LookAtTarget();
        }

        void LookAtTarget()
        {
            if (trackedItem != null)
            {
                var distance = Vector3.Distance(trackedItem.position, mainCharacter.position) / transparencyFactor;
                transparency.alpha = distance / 10f;
                var onScreenDistance = TargetPositionOnScreen() - pivotTransform.position;
                pivotTransform.rotation = Quaternion.LookRotation(Vector3.forward, onScreenDistance);

            }
            else
            {
                transparency.alpha = 0;
            }
        }
        
        Vector3 TargetPositionOnScreen()
        {
            var pos = Camera.main.WorldToScreenPoint(trackedItem.position);
            if (Vector3.Dot(trackedItem.position - Camera.main.transform.position, Camera.main.transform.forward) < 0)
            {
                if (pos.x < Screen.width / 2f)
                    pos.x = maxX;
                else
                    pos.x = minX;

                if (pos.y < Screen.height / 2f)
                    pos.y = maxY;
                else
                    pos.y = minY;
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            return pos;
        }
    }
}
