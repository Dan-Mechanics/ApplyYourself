using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class FutureMaterial : MonoBehaviour
    {
        [SerializeField] private Material[] materials = default;

        private void Start()
        {
            MeshRenderer rend = GetComponent<MeshRenderer>();
            rend.sharedMaterial = materials[0];
            
            // either string based.
            // or like index based or someth esle

            // add editor meme, met dat je preview ziet, dus like tool development
            // en like scene als geheel erin kan slepen. additive scene load wil ik.
        }
    }
}
