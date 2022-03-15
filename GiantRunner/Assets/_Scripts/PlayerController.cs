using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private SwerveInputSystem _swerveInputSystem;
    private Animator charAnim;
    public GameObject hodor;
    public GameObject _wall;
    public GameObject _start;
    public bool isRun = false;
    public bool isFight = false;
    public GameObject boy;
    [SerializeField] private Material _playerMaterial;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _swerveSpeed = 0.5f;
    [SerializeField] private float _maxSwerveAmount = 0.5f;
    public float _attackDamage = 1f;
    public float _playerHP = 15f;
    public bool isEnd = false;
    float time;
    float timer;


    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
        charAnim = GetComponent<Animator>();
        instance = this;
    }
    void Start()
    {
        time = 0.7f;
        timer = 0;

        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, 0.1f, 0.30f), Mathf.Clamp(transform.localScale.y, 0.1f, 0.30f), Mathf.Clamp(transform.localScale.z, 0.1f, 0.30f));
    }
    void Update()
    {
        if (charAnim.GetCurrentAnimatorStateInfo(0).IsName("Kicking"))
        {
            print("kick");
        }
        if (Input.GetMouseButtonDown(0) && isFight == false)
        {
            isRun = true;
        }
        if (isRun)
        {
            MoveForward();
            Swervesystem();
        }
        else if (isFight)
        {
            timer -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && timer <= 0)
            {
                StartCoroutine(Punch());
                timer = time;
            }
        }
        if (_playerHP <= 0 && !isEnd)
        {
            Death();
            isEnd = true;
        }
        //else if (_playerHP >0 && BossScript.instance._bossHP<=0)
        //{
        //    charAnim.SetBool("isWin", true);
        //}
    }

    private void MoveForward()
    {
        charAnim.SetBool("isRun", true);
        transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed, Space.World);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -0.62f, 0.62f), transform.position.y, transform.position.z);
    }
    private void Swervesystem()
    {
        float swerveAmount = _swerveInputSystem.MoveFactorX*Time.deltaTime*_swerveSpeed;
        swerveAmount = Mathf.Clamp(swerveAmount,-_maxSwerveAmount, _maxSwerveAmount);
        transform.Translate(swerveAmount, 0f, 0f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("goFight"))
        {
            isRun = false;
            isFight = true;
            charAnim.SetBool("isRun", false);
            Destroy(_wall.gameObject);

        }
        if (other.gameObject.name.Equals("trigger"))
        {
            Destroy(hodor.gameObject);
            
        }
        if (other.gameObject.CompareTag("Changer"))
        {
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = other.gameObject.GetComponent<Renderer>().material.GetColor("_Color");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TryAgain();
        }
    }

    private IEnumerator Punch()
    {
        charAnim.SetBool("isFight", true);
        yield return new WaitForSeconds(0.1f);
        charAnim.SetBool("isFight", false);
        BossScript.instance._bossHP -= _attackDamage;
        print("Boss Hp" + BossScript.instance._bossHP);
    }

    private void Death()
    {
        charAnim.SetBool("isDead", true);
    }

    public void SizeUp()
    {
        if (transform.localScale.x < 0.3f)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.02f, transform.localScale.y + 0.02f, transform.localScale.z + 0.02f);
            _attackDamage += 0.1f;
            _playerHP += 0.5f;
        }
    }
    void TryAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Kick()
    {
        charAnim.SetBool("isWin", true);
    }
}
