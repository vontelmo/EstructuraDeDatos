using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public enum PlanetName { Pluto, Mercury, Uranus, Venus, Saturn, Jupiter, Earth, Mars, Neptune, PlanetX, Ceres, Eris }

[CreateAssetMenu(fileName = "New Planet", menuName = "Planet")]

public class Planet : ScriptableObject 
{
    public Sprite Image;
    public PlanetName PlanetName;


}

