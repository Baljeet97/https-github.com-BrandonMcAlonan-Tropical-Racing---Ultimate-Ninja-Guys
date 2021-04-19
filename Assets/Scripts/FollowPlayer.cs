using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = new Vector2(player.transform.position.x, player.transform.position.y);

        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
