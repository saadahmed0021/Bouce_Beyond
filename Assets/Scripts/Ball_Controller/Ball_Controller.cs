using UnityEngine;

public class Ball_Controller : MonoBehaviour
{
    [Header("Ball Data")]
    [SerializeField] private Ball_Data ballData;

    [Header("Systems")]
    [SerializeField] private Ball_Input input;
    [SerializeField] private Ball_Movement movement;
    [SerializeField] private Ball_Physics physics;
    [SerializeField] private Ball_Jump jump;
    [SerializeField] private Ball_GroundCheck groundCheck;
    [SerializeField] private Ball_References references;

    public Ball_Data BallData => ballData;
    public Ball_Input Input => input;
    public Ball_Movement Movement => movement;
    public Ball_Physics Physics => physics;
    public Ball_Jump Jump => jump;
    public Ball_GroundCheck GroundCheck => groundCheck;
    public Ball_References References => references;

    private void Awake()
    {
        CacheSystems();
        ValidateSystems();
    }

    private void CacheSystems()
    {
        input ??= GetComponent<Ball_Input>();
        movement ??= GetComponent<Ball_Movement>();
        physics ??= GetComponent<Ball_Physics>();
        jump ??= GetComponent<Ball_Jump>();
        groundCheck ??= GetComponent<Ball_GroundCheck>();
        references ??= GetComponent<Ball_References>();
    }

    private void ValidateSystems()
    {
        if (ballData == null)
        {
            Debug.LogError("Ball_Data is missing.", this);
        }

        if (input == null)
        {
            Debug.LogError("Ball_Input is missing.", this);
        }

        if (movement == null)
        {
            Debug.LogError("Ball_Movement is missing.", this);
        }

        if (physics == null)
        {
            Debug.LogError("Ball_Physics is missing.", this);
        }

        if (jump == null)
        {
            Debug.LogError("Ball_Jump is missing.", this);
        }

        if (groundCheck == null)
        {
            Debug.LogError("Ball_GroundCheck is missing.", this);
        }

        if (references == null)
        {
            Debug.LogError("Ball_References is missing.", this);
        }
    }
}