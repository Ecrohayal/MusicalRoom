using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private DynamicJoystick joy;
    [SerializeField] private Animator anim;

    public float moveSpeed;

   
    public TextMeshProUGUI moneyText;
    public int moneyCount;
    public GameObject level,trailPartical;
    void Start()
    {
        moneyCount = 0;
        moneyText.text = moneyCount + "$".ToString();
    }


    private void FixedUpdate()
    {
        Movement();
    }
    public void Movement()
    {
        float horizontal = joy.Horizontal;
        float vertical = joy.Vertical;

        Vector3 addedPos = new Vector3(horizontal * moveSpeed * Time.deltaTime, 0, vertical * moveSpeed * Time.deltaTime);
        rb.position += addedPos;


        if (joy.Horizontal != 0 || joy.Vertical != 0)
        {
            Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;
            rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), moveSpeed * Time.deltaTime);
            anim.SetBool("running", true);
            trailPartical.SetActive(true);
        }
        else
        {
            anim.SetBool("running", false);
            trailPartical.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            moneyCount += 25;
            moneyText.text = moneyCount + "$".ToString();
            other.transform.DOScale(new Vector3(0.5f, .5f, .5f), 2f).OnComplete(() => other.transform.DOScale(new Vector3(0, 0, 0), 1.5f).OnComplete(() => other.gameObject.SetActive(false)));
        }
        
        if(other.CompareTag("LevelUp"))
        {
            other.GetComponent<BoxCollider>().isTrigger = true;
            level.SetActive(true);
        }
      
    }

    

}
