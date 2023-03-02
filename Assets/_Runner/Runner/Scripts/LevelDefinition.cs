using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;
using System.Linq;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A scriptable object that stores all information
    /// needed to load and set up a Runner level.
    /// </summary>
    [CreateAssetMenu(fileName = "Data", menuName = "Runner/LevelDefinition", order = 1)]
    public class LevelDefinition : AbstractLevelData
    {
        /// <summary>
        /// The Length of the level.
        /// </summary>
        public float LevelLength;

        /// <summary>
        /// The amount of extra terrain to be added before the start of the level.
        /// </summary>
        public float LevelLengthBufferStart = 5.0f;

        /// <summary>
        /// The amount of extra terrain to be added after the end of the level.
        /// </summary>
        public float LevelLengthBufferEnd = 5.0f;

        /// <summary>
        /// The width of the level.
        /// </summary>
        public float LevelWidth = 5.0f;

        /// <summary>
        /// The thickness of the level.
        /// </summary>
        public float LevelThickness = 0.1f;

        /*/// <summary>
        /// Space between terrain.
        /// </summary>
        public float SpaceBetweenTerrain;

        // List of MeshToCreate
        public List<MeshToCreate> ListMeshToCreate = new List<MeshToCreate>();*/

        /// <summary>
        /// True means that spawnables should snap to a grid in this level.
        /// </summary>
        public bool SnapToGrid = true;

        /// <summary>
        /// The size of the grid that spawnables will snap to if SnapToGrid 
        /// is true.
        /// </summary>
        public float GridSize = 1.0f;

        /// <summary>
        /// The material applied to the generated terrain for this level.
        /// </summary>
        public Material TerrainMaterial;

        /// <summary>
        /// A prefab placed at the start point of this level.
        /// </summary>
        public GameObject StartPrefab;

        /// <summary>
        /// A prefab placed at the end of this level. This prefab should 
        /// contain collision logic to complete the level.
        /// </summary>
        public GameObject EndPrefab;

        public AreaDefinition Area;

        /// <summary>
        /// An array of all SpawnableObjects that exist in this level.
        /// </summary>
        public SpawnableObject[] Spawnables;

        [System.Serializable]
        public class SpawnableObject
        {
            /// <summary>
            /// The prefab spawned by this SpawnableObject.
            /// </summary>
            public GameObject SpawnablePrefab;

            /// <summary>
            /// The world position of this SpawnableObject.
            /// </summary>
            public Vector3 Position = Vector3.zero;

            /// <summary>
            /// The rotational euler angles of this SpawnableObject.
            /// </summary>
            public Vector3 EulerAngles = Vector3.zero;

            /// <summary>
            /// The world scale of this SpawnableObject.
            /// </summary>
            public Vector3 Scale = Vector3.one;

            /// <summary>
            /// The base color to be applied to the materials on 
            /// this SpawnableObject.
            /// </summary>
            public Color BaseColor = Color.white;

            /// <summary>
            /// True if this object should snap to a levels grid. 
            /// Setting this value to false will make this SpawnableObject
            /// ignore the level's snap settings.
            /// </summary>
            public bool SnapToGrid = true;
        }

        /*[System.Serializable]
        public class MeshToCreate
        {
            /// <summary>
            /// The Length of the level.
            /// </summary>
            public float MeshLength = 100.0f;

            /// <summary>
            /// The amount of extra terrain to be added before the start of the level.
            /// </summary>
            public float MeshLengthBufferStart = 5.0f;

            /// <summary>
            /// The amount of extra terrain to be added after the end of the level.
            /// </summary>
            public float MeshLengthBufferEnd = 5.0f;

            *//*/// <summary>
            /// The width of the level.
            /// </summary>
            public float LevelWidth = 5.0f;*/

            /*/// <summary>
            /// The thickness of the level.
            /// </summary>
            public float MeshThickness = 0.1f;*//*
        }*/

        /// <summary>
        /// Store all values from updatedLevel into this LevelDefinition.
        /// </summary>
        /// <param name="updatedLevel">
        /// The LevelDefinition that has been edited in the Runner Level Editor Window.
        /// </param>
        public void SaveValues(LevelDefinition updatedLevel)
        {
            // TODO - Add Tests for this!
            LevelLength = updatedLevel.LevelLength;
            LevelLengthBufferStart = updatedLevel.LevelLengthBufferStart;
            LevelLengthBufferEnd = updatedLevel.LevelLengthBufferEnd;
            LevelWidth = updatedLevel.LevelWidth;
            LevelThickness = updatedLevel.LevelThickness;
            /*LevelLength = 0;
            foreach (MeshToCreate mesh in updatedLevel.ListMeshToCreate)
            {
                LevelLength += mesh.MeshLength + mesh.MeshLengthBufferEnd;
            }
            LevelLength += (ListMeshToCreate.Count - 1) * SpaceBetweenTerrain;*/

            SnapToGrid = updatedLevel.SnapToGrid;
            GridSize = updatedLevel.GridSize;
            TerrainMaterial = updatedLevel.TerrainMaterial;
            StartPrefab = updatedLevel.StartPrefab;
            EndPrefab = updatedLevel.EndPrefab;
            Spawnables = updatedLevel.Spawnables;
            //ListMeshToCreate = updatedLevel.ListMeshToCreate;
        }

        /*public float GetLevelBufferEnd()
        {
            float bufferEnd = 0;
            foreach (MeshToCreate mesh in ListMeshToCreate)
            {
                bufferEnd += mesh.MeshLengthBufferEnd;
            }

            return LevelLength - bufferEnd;
        }*/

        #region Context Menu
        [ContextMenu("Reset Gate.Bomb.Fence.Pillar Scale")]
        void ResetScale()
        {
            for (int i = 0; i < Spawnables.Length; i++)
            {
                var fkingthing = Spawnables[i].SpawnablePrefab;
                if (fkingthing != null && (fkingthing.name.Contains("Gate") || fkingthing.name.Contains("Bomb") || fkingthing.name.Contains("Fence") || fkingthing.name.Contains("Obstacle_SpinningPillar_side")))
                {
                    Spawnables[i].Scale = new Vector3(1, 1, 1);
                }
            }
        }

        [ContextMenu("Reset Pillar Rotation")]
        void ResetRotation()
        {
            for (int i = 0; i < Spawnables.Length; i++)
            {
                var fkingthing = Spawnables[i].SpawnablePrefab;
                if (fkingthing != null && (fkingthing.name.Contains("SpinningPillar_3")))
                {
                    Spawnables[i].EulerAngles = Vector3.zero;
                }
            }
        }
        #endregion
    }
}