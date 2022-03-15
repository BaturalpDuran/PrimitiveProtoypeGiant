using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public static BossScript instance;
    [SerializeField] public float _bossHP = 10f;
    private Animator _bossAnim;
    private Rigidbody rb;
    private float time;
    private float timer;
    public bool isEnd = false;
    public bool isGround = true;
    public float _bossfly = 1;
    float timer1 = 1.1f;
    public bool camControl = false;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        time = 0.7f;
        timer = 0;
        _bossAnim = GetComponent<Animator>();
    }
    void Update()
    {
     if (PlayerController.instance.isFight)
        {
            timer -= Time.deltaTime;
            if (timer < 0 && _bossHP>0)
            {
                StartCoroutine(Punch());
                timer = time;
                PlayerController.instance._playerHP -= 1.5f;
            }
        }
     
    }

    private void FixedUpdate()
    {
        if (_bossHP <= 0 && !isEnd)
        {
          Death(3f).GetAwaiter().GetResult();
        }
    }



    private IEnumerator Punch()
    {
        _bossAnim.SetBool("isFight", true);
        yield return new WaitForSeconds(0f);
        _bossAnim.SetBool("isFight", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    public async Task Death(float duration)
    {
        var end = Time.time + duration;
        while (Time.time < end)
        {
            rb.AddForce(0f, 150f, 100f * _bossfly);
            isGround = false;
            _bossAnim.SetBool("isDead", true);
            isEnd = true;
            await Task.Yield();
            camControl = true;
        }
    }

}
