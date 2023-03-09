using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class representing a Spawnable object.
    /// If a GameObject tagged "Player" collides
    /// with this object, it will trigger a Gate
    /// function with PlayerController.
    /// </summary>
    public class Gate : Spawnable
    {
        const string k_PlayerTag = "Player";
        const string k_GateTag = "Gate";

        [SerializeField]
        SoundID m_Sound = SoundID.None;

        [SerializeField]
        GateType m_GateType;
        [SerializeField]
        float m_Value;
        [SerializeField]
        RectTransform m_Text;

        bool m_Applied;
        Vector3 m_TextInitialScale;
        Gate pair;

        enum GateType
        {
            ChangeSpeed,
            ChangeSize,
            ChangeQuantity,
            Multiply,
            Divide,
            Root,
        }

        /// <summary>
        /// Sets the local scale of this spawnable object
        /// and ensures the Text attached to this gate
        /// does not scale.
        /// </summary>
        /// <param name="scale">
        /// The scale to apply to this spawnable object.
        /// </param>
        public override void SetScale(Vector3 scale)
        {
            // Ensure the text does not get scaled
            if (m_Text != null)
            {
                float xFactor = Mathf.Min(scale.y / scale.x, 1.0f);
                float yFactor = Mathf.Min(scale.x / scale.y, 1.0f);
                m_Text.localScale = Vector3.Scale(m_TextInitialScale, new Vector3(xFactor, yFactor, 1.0f));

                m_Transform.localScale = scale;
            }
        }

        private void Start()
        {
            //pair = col
            StartCoroutine(DelayRay());
        }

        IEnumerator DelayRay()
        {
            yield return new WaitForSeconds(.2f);

            var ray = new Ray(transform.position, transform.right);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log($"from {transform.name} to {hit.transform.name}", hit.transform);
                pair = hit.transform.gameObject.GetComponent<Gate>();
            }

            ray = new Ray(transform.position, -transform.right);
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log($"from {transform.name} to {hit.transform.name}", hit.transform);
                pair = hit.transform.gameObject.GetComponent<Gate>();
            }
        }

        /// <summary>
        /// Reset the gate to its initial state. Called when a level
        /// is restarted by the GameManager.
        /// </summary>
        public override void ResetSpawnable()
        {
            m_Applied = false;
        }

        protected override void Awake()
        {
            base.Awake();

            if (m_Text != null)
            {
                m_TextInitialScale = m_Text.localScale;
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(k_PlayerTag) && !m_Applied)
            {
                AudioManager.Instance.PlayEffect(m_Sound);

                ActivateGate();
            }

            if (col.CompareTag(k_GateTag))
            {
                pair = col.GetComponent<Gate>();
            }
        }

        public void SetApplyed()
        {
            m_Applied = true;
        }

        void ActivateGate()
        {
            switch (m_GateType)
            {
                case GateType.ChangeSpeed:
                    PlayerController.Instance.AdjustSpeed(m_Value);
                    break;

                case GateType.ChangeSize:
                    PlayerController.Instance.AdjustScale(m_Value);
                    break;

                case GateType.ChangeQuantity:
                    PlayerController.Instance.AdjustQuantity((int)m_Value);
                    break;
                case GateType.Multiply:
                    PlayerController.Instance.AdjustQuantity_Multiply((int)m_Value);
                    break;
                case GateType.Divide:
                    PlayerController.Instance.AdjustQuantity_Divide((int)m_Value);
                    break;
                case GateType.Root:
                    PlayerController.Instance.AdjustQuantity_NthRoot((int)m_Value);
                    break;
            }

            SetApplyed();
            pair?.SetApplyed();
        }
    }
}