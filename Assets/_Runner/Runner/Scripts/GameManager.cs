using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class used to store game state information, 
    /// load levels, and save/load statistics as applicable.
    /// The GameManager class manages all game-related 
    /// state changes.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Returns the GameManager.
        /// </summary>
        public static GameManager Instance => s_Instance;
        static GameManager s_Instance;

        [SerializeField]
        AbstractGameEvent m_WinEvent;

        [SerializeField]
        AbstractGameEvent m_LoseEvent;

        LevelDefinition m_CurrentLevel;

        /// <summary>
        /// Returns true if the game is currently active.
        /// Returns false if the game is paused, has not yet begun,
        /// or has ended.
        /// </summary>
        public bool IsPlaying => m_IsPlaying;
        bool m_IsPlaying;
        GameObject m_CurrentLevelGO;
        List<GameObject> m_CurrentTerrainGOList =  new List<GameObject>();
        GameObject m_LevelMarkersGO;

        List<Spawnable> m_ActiveSpawnables = new List<Spawnable>();

#if UNITY_EDITOR
        bool m_LevelEditorMode;
#endif

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;

#if UNITY_EDITOR
            // If LevelManager already exists, user is in the LevelEditorWindow
            if (LevelManager.Instance != null)
            {
                StartGame();
                m_LevelEditorMode = true;
            }
#endif
        }

        public LevelDefinition GetCurrentLevelDef()
        {
            return m_CurrentLevel;
        }

        /// <summary>
        /// This method calls all methods necessary to load and
        /// instantiate a level from a level definition.
        /// </summary>
        public void LoadLevel(LevelDefinition levelDefinition)
        {
            m_CurrentLevel = levelDefinition;

            LoadSkyBox();
            LoadLevel(m_CurrentLevel, ref m_CurrentLevelGO);
            CreateTerrain(m_CurrentLevel, ref m_CurrentTerrainGOList);
            PlaceLevelMarkers(m_CurrentLevel, ref m_LevelMarkersGO);
            StartGame();
        }

        private void LoadSkyBox()
        {
            if (m_CurrentLevel.Area != null)
            {
                RenderSettings.skybox = m_CurrentLevel.Area.Skybox;
                RenderSettings.fog = true;
                RenderSettings.fogMode = FogMode.Linear;
                RenderSettings.fogColor = new Color(122f / 255, 189f / 255, 221f / 255);
                //RenderSettings.fogDensity = .03f;
                RenderSettings.fogStartDistance = 40f;
                RenderSettings.fogEndDistance = 60f;
                DynamicGI.UpdateEnvironment();
            }
        }

        /// <summary>
        /// This method calls all methods necessary to restart a level,
        /// including resetting the player to their starting position
        /// </summary>
        public void ResetLevel()
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.ResetPlayer();
            }

            //if (CameraManager.Instance != null)
            //{
            //    CameraManager.Instance.ResetCamera();
            //}

            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.ResetSpawnables();
            }
        }

        /// <summary>
        /// This method loads and instantiates the level defined in levelDefinition,
        /// storing a reference to its parent GameObject in levelGameObject
        /// </summary>
        /// <param name="levelDefinition">
        /// A LevelDefinition ScriptableObject that holds all information needed to 
        /// load and instantiate a level.
        /// </param>
        /// <param name="levelGameObject">
        /// A new GameObject to be created, acting as the parent for the level to be loaded
        /// </param>
        public static void LoadLevel(LevelDefinition levelDefinition, ref GameObject levelGameObject)
        {
            if (levelDefinition == null)
            {
                Debug.LogError("Invalid Level!");
                return;
            }

            if (levelGameObject != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(levelGameObject);
                }
                else
                {
                    DestroyImmediate(levelGameObject);
                }
            }

            levelGameObject = new GameObject("LevelManager");
            LevelManager levelManager = levelGameObject.AddComponent<LevelManager>();
            levelManager.LevelDefinition = levelDefinition;

            Transform levelParent = levelGameObject.transform;

            for (int i = 0; i < levelDefinition.Spawnables.Length; i++)
            {
                LevelDefinition.SpawnableObject spawnableObject = levelDefinition.Spawnables[i];

                if (spawnableObject.SpawnablePrefab == null)
                {
                    continue;
                }
                // uncomment later
                /*MonoBehaviour script = spawnableObject.SpawnablePrefab.GetComponent<MonoBehaviour>();
                if (script is IBridge)
                {
                    continue;
                }*/

                Vector3 position = spawnableObject.Position;
                Vector3 eulerAngles = spawnableObject.EulerAngles;
                Vector3 scale = spawnableObject.Scale;

                GameObject go = null;
                
                if (Application.isPlaying)
                {
                    go = GameObject.Instantiate(spawnableObject.SpawnablePrefab, position, Quaternion.Euler(eulerAngles));
                }
                else
                {
#if UNITY_EDITOR
                    go = (GameObject)PrefabUtility.InstantiatePrefab(spawnableObject.SpawnablePrefab);
                    go.transform.position = position;
                    go.transform.eulerAngles = eulerAngles;
#endif
                }

                if (go == null)
                {
                    return;
                }

                // Set Base Color
                Spawnable spawnable = go.GetComponent<Spawnable>();
                if (spawnable != null)
                {
                    spawnable.SetBaseColor(spawnableObject.BaseColor);
                    spawnable.SetScale(scale);
                    levelManager.AddSpawnable(spawnable);
                }

                if (go != null)
                {
                    go.transform.SetParent(levelParent);
                }

                if (go.GetComponent<MonoBehaviour>() is IBridge)
                {
                    go.GetComponent<Bridge>().SetTerrainWidth(levelDefinition.LevelWidth);
                }
            }
        }

        public void UnloadCurrentLevel()
        {
            if (m_CurrentLevelGO != null)
            {
                GameObject.Destroy(m_CurrentLevelGO);
            }

            if (m_LevelMarkersGO != null)
            {
                GameObject.Destroy(m_LevelMarkersGO);
            }

            /*if (m_CurrentTerrainGOList != null)
            {
                GameObject.Destroy(m_CurrentTerrainGOList);
            }*/

            foreach (GameObject go in m_CurrentTerrainGOList)
            {
                if (Application.isPlaying)
                {
                    Destroy(go);
                }
                else
                {
                    DestroyImmediate(go);
                }
            }
            m_CurrentTerrainGOList.Clear();

            m_CurrentLevel = null;
        }

        void StartGame()
        {
            ResetLevel();
            m_IsPlaying = true;
        }

        /// <summary>
        /// Creates and instantiates the StartPrefab and EndPrefab defined inside
        /// the levelDefinition.
        /// </summary>
        /// <param name="levelDefinition">
        /// A LevelDefinition ScriptableObject that defines the start and end prefabs.
        /// </param>
        /// <param name="levelMarkersGameObject">
        /// A new GameObject that is created to be the parent of the start and end prefabs.
        /// </param>
        public static void PlaceLevelMarkers(LevelDefinition levelDefinition, ref GameObject levelMarkersGameObject)
        {
            if (levelMarkersGameObject != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(levelMarkersGameObject);
                }
                else
                {
                    DestroyImmediate(levelMarkersGameObject);
                }
            }

            levelMarkersGameObject = new GameObject("Level Markers");

            GameObject start = levelDefinition.StartPrefab;
            GameObject end = levelDefinition.EndPrefab;

            if (start != null)
            {
                GameObject go = GameObject.Instantiate(start, new Vector3(start.transform.position.x, start.transform.position.y, 0.0f), Quaternion.identity);
                go.GetComponent<BoxCollider>().isTrigger = true;
                go.transform.SetParent(levelMarkersGameObject.transform);
            }

            if (end != null)
            {
                GameObject go = GameObject.Instantiate(end, new Vector3(end.transform.position.x, end.transform.position.y, levelDefinition.LevelLength), Quaternion.identity);
                go.GetComponent<BoxCollider>().isTrigger = true;
                go.transform.SetParent(levelMarkersGameObject.transform);
            }
        }

        /// <summary>
        /// Creates and instantiates a Terrain GameObject, built
        /// to the specifications saved in levelDefinition.
        /// </summary>
        /// <param name="levelDefinition">
        /// A LevelDefinition ScriptableObject that defines the terrain size.
        /// </param>
        /// <param name="terrainGameObject">
        /// A new GameObject that is created to hold the terrain.
        /// </param>
        /*public static void CreateTerrain(LevelDefinition levelDefinition, ref GameObject terrainGameObject)
        {
            List<LevelDefinition.SpawnableObject> bridgeList = Array.FindAll(levelDefinition.Spawnables,
                                                     x => x.SpawnablePrefab != null && x.SpawnablePrefab.GetComponent<MonoBehaviour>() is IBridge).ToList();

            bridgeList =  bridgeList.OrderBy(x => x.Position.z).ToList();

            Debug.Log("bridgeeeee " + bridgeList.Count);

            float width = 0;
            float length = 0;
            float startBuffer = 0;
            float endBuffer = 0;
            float thickness = 0;

            *//*for (int i = 0; i < bridgeList.Count; i++)
            {
                Debug.Log("z pos: " + bridgeList[i].Position);



                TerrainGenerator.TerrainDimensions terrainDimensions = new TerrainGenerator.TerrainDimensions()
                {
                    Width = width,
                    Length = length,
                    StartBuffer = startBuffer,
                    EndBuffer = endBuffer,
                    Thickness = thickness
                };
                TerrainGenerator.CreateTerrain(terrainDimensions, levelDefinition.TerrainMaterial, ref terrainGameObject);
            }*//*

            TerrainGenerator.TerrainDimensions terrainDimensions = new TerrainGenerator.TerrainDimensions()
            {
                Width = levelDefinition.LevelWidth,
                Length = levelDefinition.LevelLength,
                StartBuffer = levelDefinition.LevelLengthBufferStart,
                EndBuffer = levelDefinition.LevelLengthBufferEnd,
                Thickness = levelDefinition.LevelThickness
            };
            TerrainGenerator.CreateTerrain(terrainDimensions, levelDefinition.TerrainMaterial, ref terrainGameObject);
        }*/

        public static void CreateTerrain(LevelDefinition levelDefinition, ref List<GameObject> terrainGameObjectList)
        {
            if (terrainGameObjectList != null)
            {
                foreach (GameObject go in terrainGameObjectList)
                {
                    if (Application.isPlaying)
                    {
                        Destroy(go);
                    }
                    else
                    {
                        DestroyImmediate(go);
                    }
                }
                terrainGameObjectList.Clear();
            }
            else terrainGameObjectList = new List<GameObject>();

            // get list of bridge
            List<LevelDefinition.SpawnableObject> bridgeList = Array.FindAll(levelDefinition.Spawnables,
                                                     x => x.SpawnablePrefab != null && x.SpawnablePrefab.GetComponent<IBridge>() != null).ToList();

            bridgeList = bridgeList.OrderBy(x => x.Position.z).ToList();
            
            float startPosition = 0;
            float endPosition = 0;
            var len = 0f;

            // spawn terrain
            for (int i = 0; i < bridgeList.Count + 1; i++)
            {
                if (i < bridgeList.Count)
                {
                    endPosition = bridgeList[i].Position.z;
                }
                else if (i == bridgeList.Count)
                {
                    endPosition = levelDefinition.LevelLength + levelDefinition.LevelLengthBufferEnd+6;
                }

                if (i == 0)
                {
                    startPosition = -levelDefinition.LevelLengthBufferStart;
                }

                len = endPosition - startPosition;
                var terrainDimensions = new TerrainGenerator.TerrainDimensions()
                {
                    Width = levelDefinition.LevelWidth,
                    Length = len,
                    StartBuffer = 0,
                    EndBuffer = 0,
                    Thickness = levelDefinition.LevelThickness
                };
                var terrain = TerrainGenerator.CreateTerrain(terrainDimensions, levelDefinition.Area?.Road?? levelDefinition.TerrainMaterial);
                terrain.transform.position = new Vector3(0, 0, startPosition);

                var step = 18f;
                for (var j = 0f; j <= len - step; j += step)
                {
                    var positionTarget = j - 10;
                    var positionSpawn = new Vector3(-levelDefinition.LevelWidth / 2, 0, positionTarget);
                    Instantiate(levelDefinition.Area.Fence_left, positionSpawn, Quaternion.identity, terrain.transform);
                    positionSpawn.x = -positionSpawn.x;
                    Instantiate(levelDefinition.Area.Fence_right, positionSpawn, Quaternion.identity, terrain.transform);
                }

                if (i < bridgeList.Count)
                {
                    var ibridge = bridgeList[i].SpawnablePrefab.GetComponent<IBridge>();
                    startPosition = ibridge.Length + endPosition;
                }

                //terrain.AddComponent(typeof(BoxCollider));

                if (terrainGameObjectList != null)
                {
                    terrainGameObjectList.Add(terrain);
                }
            }

            var lastTerrian = terrainGameObjectList[terrainGameObjectList.Count - 1];
            LoadBattltField(levelDefinition, lastTerrian.transform);
        }

        private static void LoadBattltField(LevelDefinition levelDefinition, Transform parent)
        {
            if (levelDefinition.Area?.BoardBattleScene)
            {
                var diff = 20f;
                var board = Instantiate(levelDefinition.Area.BoardBattleScene, parent);
                board.transform.position = new Vector3(0,.6f,levelDefinition.LevelLength+ diff);

                var positionSpawn = new Vector3(-levelDefinition.LevelWidth / 2, 0, levelDefinition.LevelLength- 7f);
                Instantiate(levelDefinition.Area.Fence_left, positionSpawn, Quaternion.identity, parent);
                positionSpawn.x = -positionSpawn.x;
                Instantiate(levelDefinition.Area.Fence_right, positionSpawn, Quaternion.identity, parent);
            }
        }

        public void Win()
        {
            m_WinEvent.Raise();

#if UNITY_EDITOR
            if (m_LevelEditorMode)
            {
                ResetLevel();
            }
#endif
        }

        public void Lose()
        {
            m_LoseEvent.Raise();

#if UNITY_EDITOR
            if (m_LevelEditorMode)
            {
                ResetLevel();
            }
#endif
        }
    }
}