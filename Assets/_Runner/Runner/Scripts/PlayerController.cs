using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class used to control a player in a Runner
    /// game. Includes logic for player movement as well as 
    /// other gameplay logic.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        /// <summary> Returns the PlayerController. </summary>
        #region Variable Declaration
        public static PlayerController Instance => s_Instance;
        static PlayerController s_Instance;

          #region Local
        //[SerializeField]
        //Animator m_Animator;
        [SerializeField]
        GameObject m_Character;

        /*[SerializeField]
        SkinnedMeshRenderer m_SkinnedMeshRenderer;*/

        [SerializeField]
        GameObject m_StartingPoint;

        [SerializeField]
        GameObject m_CharacterHolder;

        [SerializeField]
        PlayerSpeedPreset m_PlayerSpeed = PlayerSpeedPreset.Medium;

        [SerializeField]
        float m_CustomPlayerSpeed = 10.0f;

        [SerializeField]
        float m_AccelerationSpeed = 10.0f;

        [SerializeField]
        float m_DecelerationSpeed = 20.0f;

        [SerializeField]
        float m_HorizontalSpeedFactor = 0.5f;

        [SerializeField]
        float m_ScaleVelocity = 2.0f;

        [SerializeField]
        bool m_AutoMoveForward = true;

        Vector3 m_LastPosition;
        float m_StartHeight;

        const float k_MinimumScale = 0.1f;
        static readonly string s_Speed = "Speed";
        static readonly string characterLayer = "Main Character";

        enum PlayerSpeedPreset
        {
            Slow,
            Medium,
            Fast,
            Custom
        }

        Transform m_Transform;
        Vector3 m_StartPosition;
        bool m_HasInput;
        float m_MaxXPosition;
        float m_XPos;
        float m_ZPos;
        float m_TargetPosition;
        float m_Speed;
        float m_TargetSpeed;
        Vector3 m_Scale;
        Vector3 m_TargetScale;
        Vector3 m_DefaultScale;

        GameObject m_FirstCharacter;
        [SerializeField] List<GameObject> m_Characters;

        const float k_HalfWidth = 0.5f;
        #endregion

          #region Public
        /// <summary> The player's root Transform component. </summary>
        public Transform Transform => m_Transform;

        /// <summary> The player's current speed. </summary>
        public float Speed => m_Speed;

        /// <summary> The player's target speed. </summary>
        public float TargetSpeed => m_TargetSpeed;

        /// <summary> The player's minimum possible local scale. </summary>
        public float MinimumScale => k_MinimumScale;

        /// <summary> The player's current local scale. </summary>
        public Vector3 Scale => m_Scale;

        /// <summary> The player's target local scale. </summary>
        public Vector3 TargetScale => m_TargetScale;

        /// <summary> The player's default local scale. </summary>
        public Vector3 DefaultScale => m_DefaultScale;

        /// <summary> The player's default local height. </summary>
        public float StartHeight => m_StartHeight;

        /// <summary> The player's default local height. </summary>
        public float TargetPosition => m_TargetPosition;

        /// <summary> The player's maximum X position. </summary>
        public float MaxXPosition => m_MaxXPosition;

        /// <summary> Default character. </summary>
        public GameObject FirstCharacter => m_FirstCharacter;

        public List<GameObject> Characters
        {
            get 
            {
                if(m_Characters == null)
                {
                    m_Characters = new List<GameObject>();
                    AddCharacter(Instantiate(m_Character.gameObject, m_StartingPoint.transform.localPosition, Quaternion.identity, m_CharacterHolder.transform));
                }
                return m_Characters;
            }
        }
          #endregion

        #endregion

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;

            Initialize();

            if (GameSceneLoad.Instance != null)
            {
                GameSceneLoad.Instance.sceneRuns.Add(this.gameObject);
            }
        }

        private void OnDestroy()
        {
            if (GameSceneLoad.Instance != null)
            {
                GameSceneLoad.Instance.sceneRuns.Remove(this.gameObject);
            }
        }

        /// <summary>
        /// Set up all necessary values for the PlayerController.
        /// </summary>
        public void Initialize()
        {
            m_Transform = transform;
            m_StartPosition = m_Transform.position;
            m_DefaultScale = m_Transform.localScale;
            m_Scale = m_DefaultScale;
            m_TargetScale = m_Scale;

            /*if (m_SkinnedMeshRenderer != null)
            {
                m_StartHeight = m_SkinnedMeshRenderer.bounds.size.y;
            }
            else
            {
                m_StartHeight = 1.0f;
            }*/

            ResetSpeed();
        }

        /// <summary>
        /// Returns the current default speed based on the currently
        /// selected PlayerSpeed preset.
        /// </summary>
        public float GetDefaultSpeed()
        {
            switch (m_PlayerSpeed)
            {
                case PlayerSpeedPreset.Slow:
                    return 5.0f;

                case PlayerSpeedPreset.Medium:
                    return 10.0f;

                case PlayerSpeedPreset.Fast:
                    return 20.0f;
            }

            return m_CustomPlayerSpeed;
        }

        /// <summary>
        /// Adjust the player's current speed
        /// </summary>
        public void AdjustSpeed(float speed)
        {
            m_TargetSpeed += speed;
            m_TargetSpeed = Mathf.Max(0.0f, m_TargetSpeed);
        }

        /// <summary>
        /// Reset the player's current speed to their default speed
        /// </summary>
        public void ResetSpeed()
        {
            m_Speed = 0.0f;
            m_TargetSpeed = GetDefaultSpeed();
        }

        /// <summary>
        /// Adjust the player's current scale
        /// </summary>
        public void AdjustScale(float scale)
        {
            m_TargetScale += Vector3.one * scale;
            m_TargetScale = Vector3.Max(m_TargetScale, Vector3.one * k_MinimumScale);
        }

        /// <summary>
        /// Reset the player's current speed to their default speed
        /// </summary>
        public void ResetScale()
        {
            m_Scale = m_DefaultScale;
            m_TargetScale = m_DefaultScale;
        }

        /// <summary>
        /// Adjust the player's current character quantity.
        /// </summary>
        public void AdjustQuantity(int numberAdd)
        {
            if(numberAdd > 0)
            {
                var currentPosition = m_StartingPoint.transform.position;
                for (int i = 0; i < numberAdd; i++)
                {
                    var newPos = currentPosition;
                    newPos.x += Random.Range(0.5f, 1) * (Random.Range(0f, 1f) < .5f ? -1 : 1);
                    newPos.z += Random.Range(0.5f, 1) * (Random.Range(0f, 1f) < .5f ? -1 : 1);
                    AddCharacter(Instantiate(m_Character.gameObject, newPos, Quaternion.identity));
                }
            }
            else if (numberAdd < 0)
            {
                for(int i = 0; i < Mathf.Abs(numberAdd); i++)
                {
                    if (Characters.Count > 0)
                    {
                        var indexRemove = Characters.Count - 1;
                        RemoveCharacter(Characters[indexRemove]);
                    }
                }
            }
            //m_TargetScale += Vector3.one * scale;
            //m_TargetScale = Vector3.Max(m_TargetScale, Vector3.one * k_MinimumScale);
        }

        public GameObject GetClosest(Vector3 positionTarget)
        {
            var target = Characters.OrderBy(go => Vector3.Distance(go.transform.position, positionTarget)).FirstOrDefault();

            RemoveCharacter(target,false);

            return target;
        }

        public void AddCharacter(GameObject newCharacter)
        {
            if (Characters.Contains(newCharacter))
            {
                Debug.LogWarning("This character was added");
            }
            else
            {
                newCharacter.layer = LayerMask.NameToLayer(characterLayer);
                newCharacter.transform.SetParent(m_CharacterHolder.transform);
                
                var anim = newCharacter.GetComponentInChildren<Animator>();
                if (anim)
                {
                    anim.SetFloat("Velocity", 1);
                }

                var rigbod = newCharacter.GetComponent<Rigidbody>();
                if (rigbod)
                {
                    rigbod.freezeRotation = true;
                }

                Characters.Add(newCharacter);
            }
        }

        /// <summary>
        /// Remove character gameobject and call Lose event when the number of characters reach 0.
        /// </summary>
        public void RemoveCharacter(GameObject characterRemove, bool isRemove =true)
        {
            if (Characters.Contains(characterRemove))
            {
                Characters.Remove(characterRemove);
                if(isRemove)
                    Destroy(characterRemove);

                if (Characters.Count <= 0)
                    GameManager.Instance.Lose();
            }
            else
            {
                Debug.LogWarning("Can't find character");
            }
        }

        /// <summary>
        /// Reset the player's character quantity to default.
        /// </summary>
        public void ResetQuantity()
        {
            m_FirstCharacter = Instantiate(m_Character.gameObject, m_StartingPoint.transform.localPosition, Quaternion.identity);
            AddCharacter(m_FirstCharacter);
        }

        /// <summary>
        /// Returns the player's transform component
        /// </summary>
        public Vector3 GetPlayerTop()
        {
            return m_Transform.position + Vector3.up * (m_StartHeight * m_Scale.y - m_StartHeight);
        }

        /// <summary>
        /// Sets the target X position of the player
        /// </summary>
        public void SetDeltaPosition(float normalizedDeltaPosition)
        {
            if (m_MaxXPosition == 0.0f)
            {
                Debug.LogError("Player cannot move because SetMaxXPosition has never been called or Level Width is 0. If you are in the LevelEditor scene, ensure a level has been loaded in the LevelEditor Window!");
            }

            float fullWidth = m_MaxXPosition * 2.0f;
            var limit = new Vector2(-m_MaxXPosition, m_MaxXPosition);
            if (Characters.Count > 0)
            {
                var pos = m_Character.transform.position.x;
                limit.x = limit.x - Characters.Min(c => c.transform.position.x) + pos;
                limit.y = limit.y - Characters.Max(c => c.transform.position.x) + pos;
            }


            m_TargetPosition = m_TargetPosition + fullWidth * normalizedDeltaPosition;
            m_TargetPosition = Mathf.Clamp(m_TargetPosition, limit.x, limit.y);
            m_HasInput = true;
        }

        /// <summary>
        /// Stops player movement
        /// </summary>
        public void CancelMovement()
        {
            m_HasInput = false;
        }

        /// <summary>
        /// Set the level width to keep the player constrained
        /// </summary>
        public void SetMaxXPosition(float levelWidth)
        {
            // Level is centered at X = 0, so the maximum player
            // X position is half of the level width
            m_MaxXPosition = levelWidth * k_HalfWidth;
        }

        /// <summary>
        /// Returns player to their starting position
        /// </summary>
        public void ResetPlayer()
        {
            m_Transform.position = m_StartPosition;
            m_XPos = 0.0f;
            m_ZPos = m_StartPosition.z;
            m_TargetPosition = 0.0f;

            m_LastPosition = m_Transform.position;

            m_HasInput = false;

            ResetQuantity();
            ResetSpeed();
            ResetScale();
        }

        void Update()
        {
            if (!GameSceneLoad.Instance.isPlaying)
            {
                return;
            }

            float deltaTime = Time.deltaTime;

            // Update Scale

            //if (!Approximately(m_Transform.localScale, m_TargetScale))
            //{
            //    m_Scale = Vector3.Lerp(m_Scale, m_TargetScale, deltaTime * m_ScaleVelocity);
            //    m_Transform.localScale = m_Scale;
            //}

            //// Update Speed

            //if (!m_AutoMoveForward && !m_HasInput)
            //{
            //    Decelerate(deltaTime, 0.0f);
            //}
            //else if (m_TargetSpeed < m_Speed)
            //{
            //    Decelerate(deltaTime, m_TargetSpeed);
            //}
            //else if (m_TargetSpeed > m_Speed)
            //{
            //    Accelerate(deltaTime, m_TargetSpeed);
            //}
            m_Speed = m_CustomPlayerSpeed;
            float speed = m_Speed * deltaTime;

            // Update position

            m_ZPos += speed;

            if (m_HasInput)
            {
                float horizontalSpeed = speed * m_HorizontalSpeedFactor;

                float newPositionTarget = Mathf.Lerp(m_XPos, m_TargetPosition, horizontalSpeed);
                float newPositionDifference = newPositionTarget - m_XPos;

                newPositionDifference = Mathf.Clamp(newPositionDifference, -horizontalSpeed, horizontalSpeed);

                m_XPos += newPositionDifference;
            }

            m_Transform.position = new Vector3(m_XPos, m_Transform.position.y, m_ZPos);

            //if (m_Animator != null && deltaTime > 0.0f)
            //{
            //    float distanceTravelledSinceLastFrame = (m_Transform.position - m_LastPosition).magnitude;
            //    float distancePerSecond = distanceTravelledSinceLastFrame / deltaTime;

            //    //m_Animator.SetFloat(s_Speed, distancePerSecond);
            //}

            //if (m_Transform.position != m_LastPosition)
            //{
            //    m_Transform.forward = Vector3.Lerp(m_Transform.forward, (m_Transform.position - m_LastPosition).normalized, speed);
            //}

            m_LastPosition = m_Transform.position;
        }

    }
}