using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HyperCasual.Runner
{
    public class Bridge : Spawnable, IBridge, ISwitchable
    {
        [Header("Reference")]
        [SerializeField] private GameObject bridge;
        [Header("Data modifed")]
        [SerializeField] private Vector3 scale;

        [SerializeField] float lengthBridge;
        [SerializeField] float duration =1f;

        public float Length
        {
            get
            {
                return lengthBridge;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            bridge.transform.localScale = Vector3.zero;
        }


        public override void ResetSpawnable()
        {
            base.ResetSpawnable();
            bridge.transform.localScale = Vector3.zero;
        }

        public void Active()
        {
            ScaleBridge();
        }

        void ScaleBridge()
        {
            DOVirtual.Float(0, Length, duration, value => {
                var targetScale = value;

                bridge.transform.localScale = new Vector3(scale.x, scale.y, targetScale);
                bridge.transform.localPosition = new Vector3(0, -.5f, targetScale/2);
            });
        }

    }
}
