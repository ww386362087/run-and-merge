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
        [SerializeField] float lengthScaleFactor = 13.89f;

        public float Length
        {
            get
            {
                return lengthBridge;
            }
        }

        public void SetTerrainWidth(float width)
        {
            //terrain_width = width;
        }

        protected override void Awake()
        {
            base.Awake();
            bridge.transform.GetChild(0).localScale = Vector3.zero;
        }

        public override void ResetSpawnable()
        {
            base.ResetSpawnable();
            bridge.transform.GetChild(0).localScale = Vector3.zero;
        }

        public void Active()
        {
            ScaleBridge();
        }

        void ScaleBridge()
        {
            Transform ground = bridge.transform.GetChild(0);

            float scaleFactor = Length ;

            var initStart = -7.15f;
            var scaleTarget = 1;
            DOVirtual.Float(0, scaleTarget, duration, value =>
            {
                ground.localScale = new Vector3(1, 1, value);
                ground.localPosition = new Vector3(0, -0.3f, initStart * (1 - value));
            });
        }

    }
}
