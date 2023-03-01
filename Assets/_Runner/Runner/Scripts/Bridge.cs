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

        float terrain_width;

        public float Length
        {
            get
            {
                return lengthBridge;
            }
        }

        public void SetTerrainWidth(float width)
        {
            terrain_width = width;
        }

        protected override void Awake()
        {
            base.Awake();
            //bridge.transform.localScale = Vector3.zero;
        }

        public override void ResetSpawnable()
        {
            base.ResetSpawnable();
            //bridge.transform.localScale = Vector3.zero;
        }

        public void Active()
        {
            ScaleBridge();
        }

        void ScaleBridge()
        {
            Transform ground = bridge.transform.GetChild(0);
            Transform handRail = bridge.transform.GetChild(1);
            Transform left_front_pillar = bridge.transform.GetChild(2);
            Transform left_back_pillar = bridge.transform.GetChild(3);
            Transform right_front_pillar = bridge.transform.GetChild(4);
            Transform right_back_pillar = bridge.transform.GetChild(5);

            float scaleFactor = Length / lengthScaleFactor;

            left_front_pillar.localScale = new Vector3(scale.x, scale.x, scale.x);
            left_front_pillar.localPosition = new Vector3(left_front_pillar.localPosition.x,
                                                          left_front_pillar.localPosition.y - 0.2f,
                                                          left_front_pillar.localPosition.z);

            left_back_pillar.localScale = new Vector3(scale.x, scale.x, scale.x);
            left_back_pillar.localPosition = new Vector3(left_back_pillar.localPosition.x,
                                                         left_back_pillar.localPosition.y - 0.2f,
                                                         left_back_pillar.localPosition.z);

            right_front_pillar.localScale = new Vector3(scale.x, scale.x, scale.x);
            right_front_pillar.localPosition = new Vector3(right_front_pillar.localPosition.x,
                                                           right_front_pillar.localPosition.y - 0.2f,
                                                           right_front_pillar.localPosition.z);

            right_back_pillar.localScale = new Vector3(scale.x, scale.x, scale.x);
            right_back_pillar.localPosition = new Vector3(right_back_pillar.localPosition.x,
                                                          right_back_pillar.localPosition.y - 0.2f,
                                                          right_back_pillar.localPosition.z);

            DOVirtual.Float(0, scaleFactor, duration, value => {
                var targetScale = value;

                //bridge.transform.localScale = new Vector3(scale.x, scale.y, targetScale);
                //bridge.transform.localPosition = new Vector3(0, -.5f, targetScale/2);

                ground.localScale = new Vector3(scale.x, scale.y, targetScale);
                ground.localPosition = new Vector3(0, -0.3f, targetScale / 2);

                handRail.localScale = new Vector3(scale.x, scale.y, targetScale);
                handRail.localPosition = new Vector3(0, -0.3f, targetScale / 2);

                left_front_pillar.localPosition = new Vector3(ground.localPosition.x - terrain_width/2,
                                                              left_front_pillar.localPosition.y,
                                                              ground.localPosition.z + Length/2);

                left_back_pillar.localPosition = new Vector3(ground.localPosition.x - terrain_width / 2,
                                                             left_back_pillar.localPosition.y,
                                                             ground.localPosition.z - Length / 2);

                right_front_pillar.localPosition = new Vector3(ground.localPosition.x + terrain_width / 2,
                                                               right_front_pillar.localPosition.y,
                                                               ground.localPosition.z + Length / 2);

                right_back_pillar.localPosition = new Vector3(ground.localPosition.x + terrain_width / 2,
                                                              right_back_pillar.localPosition.y,
                                                              ground.localPosition.z - Length / 2);
            });
        }

    }
}
