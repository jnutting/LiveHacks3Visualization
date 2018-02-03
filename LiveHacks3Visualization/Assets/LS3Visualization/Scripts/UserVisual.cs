using UnityEngine;
using DG.Tweening;

public class UserVisual: MonoBehaviour {

    public TextMesh nameMesh;
    public Transform visual;

    private float upness;

    public float Upness
    {
        get
        {
            return upness;
        }

        set
        {
            upness = value;
            //visual.transform.localEulerAngles = new Vector3(90, 0, (1.0f - upness) * 180f);
            if (upness < 0.3) {
                TextVisible = true;
            } else {
                TextVisible = false;
            }
        }
    }

    private string colorName;

    private Vector3 basePosition;

    private DeviceVelocity velocity;

    private string name;
    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
            nameMesh.text = name;
        }
    }

    private bool textVisible = false;
    public bool TextVisible
    {
        get
        {
            return textVisible;
        }

        set
        {
            if (textVisible != value) {
                textVisible = value;
                if (value) {
                    Color color = nameMesh.color;
                    color.a = 1f;
                    DOTween.To(()=> nameMesh.color, x=> nameMesh.color = x, color, 0.5f);
                } else {
                    Color color = nameMesh.color;
                    color.a = 0f;
                    DOTween.To(()=> nameMesh.color, x=> nameMesh.color = x, color, 0.5f);
                }
            }
        }
    }

    public string ColorName
    {
        get
        {
            return colorName;
        }

        set
        {
            colorName = value;
            Color color;
            if (colorName == "Yellow") {
                color = new Color(255, 237, 0);
            } else if (colorName == "Red") {
                color = new Color(190, 22, 33);
            } else if (colorName == "Cyan") {
                color = new Color(54, 169, 224);
            } else if (colorName == "Purple-Blue") {
                color = new Color(54, 169, 224);
            }
            else {
                Debug.Log("Unknown colorName " + colorName);
                return;
            }
            DOTween.To(()=> nameMesh.color, x=> nameMesh.color = x, color, 0.5f);
        }
    }

    public Vector3 BasePosition
    {
        get
        {
            return basePosition;
        }

        set
        {
            basePosition = value;
        }
    }

    public DeviceVelocity Velocity
    {
        get
        {
            return velocity;
        }

        set
        {
            velocity = value;
            //Vector3 position = basePosition;
            //position.x += velocity.x;
            //position.y += velocity.y;
            visual.localPosition = new Vector3(-0.5f * velocity.x, -0.5f * velocity.y, 0);
        }
    }
}