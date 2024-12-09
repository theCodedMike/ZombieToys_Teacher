using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        offset = player.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.Instance.GameOver)
            return;

        transform.position = player.position - offset;
    }
}
