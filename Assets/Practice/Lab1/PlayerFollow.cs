using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField][Min(0)] float Speed = .95f; 

    Vector3 initialState;
    void Start()
    {
        initialState = transform.position - Target.position;
    }


    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + initialState, Time.deltaTime * Speed);
    }
}
