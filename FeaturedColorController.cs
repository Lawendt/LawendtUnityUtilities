using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Holds a color pallete
/// </summary>
[Serializable]
public class FeaturedColorPalette
{
    public List<Color> featuredColors = new List<Color>();

    public FeaturedColorPalette(int colorsQuantity)
    {
        //Debug.Log("Constructor of featuredColorPalette");
        for (int i = 0; i < colorsQuantity; i++)
        {
            featuredColors.Add(Color.white);
        }
    }

    public void Validade(int colorsQuantity)
    {
        if (featuredColors.Count > colorsQuantity)
        {
            while (featuredColors.Count > colorsQuantity)
            {
                featuredColors.RemoveAt(featuredColors.Count - 1);
            }
        }
        else if (featuredColors.Count < colorsQuantity)
        {
            featuredColors.Add(Color.white);
        }
    }
}

/// <summary>
/// Holds a color pallete
/// Developed by Lawendt and Fabio Damian. 
/// Available @ https://github.com/Lawendt/UnityLawUtilities
/// </summary>
public class FeaturedColorController : Singleton<FeaturedColorController>
{
    [Range(1, 5)]
    public int colorsPerPalette = 1;
    [HideInInspector]
    public List<FeaturedColorPalette> featuredColorsPalettes = new List<FeaturedColorPalette>();

    public FeaturedColorController()
    {
        colorsPerPalette = 2;
        if (featuredColorsPalettes.Count == 0)
        {
            for (int i = 0; i < 10; i++)
                featuredColorsPalettes.Add(new FeaturedColorPalette(colorsPerPalette));

            SetRandomColors();
        }
    }
    public int Count
    {
        get
        {

            return featuredColorsPalettes.Count;
        }
    }
    public Color GetColor(string sectionId)
    {
        return GetColor(int.Parse(sectionId) - 1, 0);
    }

    public Color GetColor(int i, int j = 0)
    {
        return featuredColorsPalettes[i].featuredColors[j];
    }

    [ContextMenu("SetRandomColors")]
    public void SetRandomColors()
    {
        float h = 0, s = 1, v = 1;

        for (int i = 0; i < featuredColorsPalettes.Count; i++)
        {
            /* Color c = UnityEngine.Random.ColorHSV(0.0f, 1.0f, 0.88f, 0.88f, 1.0f, 1.0f);
            featuredColorsPalettes[i].featuredColors[0] = c;*/
            h += 1.0f / featuredColorsPalettes.Count;
            s = 0.85f;
            v = 0.75f;
            for (int j = 0; j < featuredColorsPalettes[i].featuredColors.Count; j++)
            {
                featuredColorsPalettes[i].featuredColors[j] = Color.HSVToRGB(h, s, v);
                s -= 0.6f / featuredColorsPalettes[i].featuredColors.Count;
                v -= 0.6f / featuredColorsPalettes[i].featuredColors.Count;
            }
        }
    }

    void OnValidate()
    {
        for (int i = 0; i < featuredColorsPalettes.Count; i++)
        {
            featuredColorsPalettes[i].Validade(colorsPerPalette);
        }
    }
}