using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BallDisplayScript : MonoBehaviour
{
    public GameManagerScript gameManager;

    public RectTransform canvasTransform;
    public Image prefab;
    public Image origin;

    private Vector2 ballAnchor;
    private List<GameObject> ballObjects;
    public List<Image> ballImages;

    // Start is called before the first frame update
    void Start()
    {
        ballAnchor = origin.rectTransform.anchoredPosition;

        foreach (Image img in ballImages)
        {
            img.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBallList(List<GameObject> balls)
    {
        if (ballObjects == null || !AreListsEqual(ballObjects, balls))
        {
            ballObjects = new List<GameObject>(balls);
            UpdateBallDisplay();
        }
    }

    private void UpdateBallDisplay()
    {
        foreach (Image img in ballImages)
        {
            img.enabled = false;
        }

        for (int i = 0; i < ballObjects.Count; i++)
        {
            ballImages[i].enabled = true;

            Texture iconTexture = EditorGUIUtility.GetIconForObject(ballObjects[i]);

            if (iconTexture != null)
            {
                Sprite iconSprite = Sprite.Create((Texture2D)iconTexture, new Rect(0, 0, iconTexture.width, iconTexture.height), new Vector2(0.5f, 0.5f));
                ballImages[i].sprite = iconSprite;
            }
        }
    }

    private bool AreListsEqual(List<GameObject> list1, List<GameObject> list2)
    {
        if (list1.Count != list2.Count)
        {
            return false;
        }

        // Compare each element (considering the reference or using a custom equality check)
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i]) // or use a custom equality method if needed
            {
                return false;
            }
        }
        return true;
    }
}
