using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Image))]
public class ChangeAssetDynamically : MonoBehaviour
{
    private Image image;
    private string nameOfImg;

    void Start()
    {
        image = GetComponent<Image>();
        nameOfImg = gameObject.name;

        string pathToFile = Path.Combine(Application.streamingAssetsPath, $"{nameOfImg}.png");

        if (!File.Exists(pathToFile))
        {
            throw new System.IO.FileNotFoundException($"No file on path {pathToFile} with name '{nameOfImg}' exists! Using default loaded images...");
        }

        byte[] bytes = File.ReadAllBytes(pathToFile);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);

        image.sprite = Sprite.Create(tex, new UnityEngine.Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100f);
    }
}