using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SkinSO", menuName = "Skins", order = 51)]
public class SkinSo : ScriptableObject
{
    [SerializeField] List<Sprite> toilets;
    [SerializeField] List<Sprite> faces;

    public KeyValuePair<Sprite, Sprite>[] Sprites()
    {
        var result = new KeyValuePair<Sprite, Sprite>[toilets.Count];

        for(int i = 0; i < toilets.Count; i++)
        {
            result[i] = new KeyValuePair<Sprite, Sprite>(toilets[i], faces[i]);
        }

        return result;
    }
}
