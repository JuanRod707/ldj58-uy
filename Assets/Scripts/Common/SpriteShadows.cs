using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.Vfx
{
    public class SpriteShadows : MonoBehaviour
    {
        void Start()
        {
            var renderer = GetComponent<SpriteRenderer>();
            renderer.shadowCastingMode = ShadowCastingMode.TwoSided;
            renderer.receiveShadows = true;
        }
    }
}
