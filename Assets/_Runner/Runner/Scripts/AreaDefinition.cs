using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AreaDefinition", menuName = "Runner/Area data", order = 1)]
public class AreaDefinition : ScriptableObject
{
    [SerializeField] private Material skybox;
    [SerializeField] private Material road;
    [SerializeField] private GameObject fence_left;
    [SerializeField] private GameObject fence_right;
    [SerializeField] private GameObject boardBattleScene;
    [SerializeField] private Color colorFog;

    public Material Skybox { get => skybox; }
    public GameObject BoardBattleScene { get => boardBattleScene; }
    public GameObject Fence_left { get => fence_left; }
    public GameObject Fence_right { get => fence_right; }
    public Material Road { get => road; }
    public Color ColorFog { get => colorFog;  }
}
