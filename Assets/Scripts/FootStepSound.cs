using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] walkClips;
    public AudioClip[] runClips;
    public float walkStepInterval = 0.5f;
    public float runStepInterval = 0.3f;
    public KeyCode runKey = KeyCode.LeftShift;

    private CharacterController controller;
    private float stepTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        stepTimer = walkStepInterval;
    }

    void Update()
    {
        float speed = controller.velocity.magnitude;

        if (controller.isGrounded && speed > 0.1f)
        {
            stepTimer -= Time.deltaTime;

            bool isRunning = Input.GetKey(runKey);
            float currentInterval = isRunning ? runStepInterval : walkStepInterval;

            if (stepTimer <= 0f)
            {
                PlayFootstep(isRunning);
                stepTimer = currentInterval;
            }
        }
        else
        {
            stepTimer = walkStepInterval;
        }
    }

    void PlayFootstep(bool running)
    {
        AudioClip[] clips = running ? runClips : walkClips;
        if (clips.Length > 0)
        {
            int index = Random.Range(0, clips.Length);
            audioSource.PlayOneShot(clips[index]);
        }
    }
}