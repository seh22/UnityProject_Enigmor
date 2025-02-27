using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using UnityEngine.TextCore.Text;

[Serializable]
public class StageImgData
{
    public List<EpisodeImgData> episodes;
}
[Serializable]
public class EpisodeImgData
{
    public Sprite episodeSprite;
}


/*[Serializable]
public class StageData
{
    public int stage;
    public List<EpisodeData> episodes;
}

public class EpisodeData
{
    public int episode;
    public List<string> intro_story;
    public List<string> hints;
    public List<string> evidence;
    public List<Character> characters;
    public List<Location> locations;
    public List<Weapon> weapons;
    public Culprit culprit;
}


[Serializable]
public class Character
{
    public int character_id;
    public string name;
    public List<string> traits;
}

[Serializable]
public class Location
{
    public int location_id;
    public string name;
    public List<string> traits;
}

[Serializable]
public class Weapon
{
    public int weapon_id;
    public string name;
    public List<string> traits;
}

[Serializable]
public class Culprit
{
    public int character_id;
    public int weapon_id;
    public int location_id;
}
*/