using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float moveDistance = 2f;     // How far it moves up and down
    public float speed = 1f;            // Speed of movement

    private Vector3 startPos;


    //miauw
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 90) * Time.deltaTime);

        float lerpValue = Mathf.PingPong(Time.time * speed, 1f); // oscillates between 0 and 1
        float newY = Mathf.Lerp(startPos.y, startPos.y + moveDistance, lerpValue);
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
