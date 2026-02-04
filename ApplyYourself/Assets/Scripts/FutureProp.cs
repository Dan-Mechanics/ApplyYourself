using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class FutureProp : MonoBehaviour
    {
        [SerializeField] private Material[] materials = default;

        private void Start()
        {
            MeshRenderer rend = GetComponent<MeshRenderer>();
            rend.sharedMaterial = materials[0];
            
            // either string based.
            // or like index based or someth esle
        }
    }
}
