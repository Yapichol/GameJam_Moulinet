using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SceneData", menuName = "Persistence")]
public class SceneData : ScriptableObject
{
    public float Score = 0;
    public float Bourse = 0;
    public float[] Price = new float[ ]{10, 20, 30, 40, 50, 60};
    public List<string> ActiveItems = new List<string>();
}
