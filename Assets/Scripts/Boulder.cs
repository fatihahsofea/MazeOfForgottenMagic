using UnityEngine;

public class Boulder : MonoBehaviour
{
    public GameObject boulder;
    private GameObject clone;
    public float zDistance = 5f;
    public float yDistance = 5f;
    private Vector3 scaleChange = new Vector3(0.001f, 0.001f, 0.001f);

    public AudioSource audioSource;      // Assign this in Inspector
    public AudioClip dropSound;          // Assign your sound clip here

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            float zPosition = transform.position.z + zDistance;
            float yPosition = transform.position.y + yDistance;
            float xPosition = transform.position.x;
            clone = Instantiate(boulder, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
        }

        if (Input.GetButton("Fire1") && clone != null)
        {
            clone.transform.localScale += scaleChange;
        }

        if (Input.GetButtonUp("Fire1") && clone != null)
        {
            clone.GetComponent<Rigidbody>().useGravity = true;

            if (audioSource != null && dropSound != null)
            {
                audioSource.PlayOneShot(dropSound);
            }
        }
    }
}
