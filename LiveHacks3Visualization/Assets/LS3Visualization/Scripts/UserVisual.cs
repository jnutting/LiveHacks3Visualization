using UnityEngine;
using DG.Tweening;

public class UserVisual: MonoBehaviour {

    public TextMesh nameMesh;
    public Transform visual;
	public Rigidbody rb;

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
                color = new Color(39, 52, 138);
            } else {
                Debug.Log("Unknown colorName " + colorName);
                return;
            }
            Debug.Log("Setting named color " + colorName + " " + color);
            MeshRenderer renderer = visual.GetComponent<MeshRenderer>();
            Color oldColor = renderer.material.color;
            Material newMaterial = new Material(renderer.material);
            newMaterial.color = oldColor;
            renderer.material = newMaterial;
            DOTween.To(()=> newMaterial.color, x=> newMaterial.color = x, color, 0.5f);
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
			//rb.AddForce (new Vector3(velocity.x, velocity.y, 0));

            Vector3 position = basePosition;
            position.x = Mathf.Clamp(position.x + velocity.x * -0.5f, position.x - 1f, position.x + 1f);
            position.y = Mathf.Clamp(position.y + velocity.y * -0.5f, position.y - 1f, position.y + 1f);
            transform.position = position;
        }
    }
}