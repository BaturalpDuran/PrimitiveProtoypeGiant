using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator _camAnim;
    public GameObject _boss;
    public GameObject _player;

    private void Awake()
    {
        _camAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.isFight)
        {
            _camAnim.SetBool("isFight", true);
        }
        if (BossScript.instance.camControl)
        {
            transform.position = new Vector3(_boss.transform.position.x +2f,_boss.transform.position.y+1f,_boss.transform.position.z);
        }
    }
}
