using System;
using UnityEngine;

[Serializable]

public enum PlanetName { Pluto, Mercury, Uranus, Venus, Saturn, Jupiter, Earth, Mars, Neptune, PlanetX, Ceres, Eris }

[CreateAssetMenu(fileName = "New Planet", menuName = "Planet")]

public class Planet : ScriptableObject, IEquatable<Planet>
{
    public Sprite Image;
    public PlanetName PlanetName;

    public bool Equals(Planet other)
    {
        return this == other;
        //return PlanetName == other.PlanetName;
    }
}